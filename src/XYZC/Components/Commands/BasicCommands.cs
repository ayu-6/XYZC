using XYZC.Core;
using System.Globalization;

namespace XYZC.Components.Commands;

public class BasicCommands
{
    public static void Import()
    {
        Commander.AddCommand("color", CommandColor);
        Commander.AddCommand("background", CommandBackground);
        Commander.AddCommand("effect", CommandEffect);
    }

    private static void CommandColor(string[] value)
    {
        if (value.Length == 0) return;
        string input = value[0].ToLower();

        if (input == "reset")
        {
            Console.Write("\x1b[39m"); // Reset foreground
            return;
        }

        if (input.StartsWith("rgb:"))
        {
            var rgb = ParseRgb(input[4..]);
            if (rgb != null)
                Console.Write($"\x1b[38;2;{rgb[0]};{rgb[1]};{rgb[2]}m");
            return;
        }

        if (input.StartsWith("#"))
        {
            var rgb = ParseHex(input);
            if (rgb != null)
                Console.Write($"\x1b[38;2;{rgb[0]};{rgb[1]};{rgb[2]}m");
            return;
        }

        if (int.TryParse(input, out int ansi))
        {
            Console.Write($"\x1b[38;5;{ansi}m");
            return;
        }

        if (Enum.TryParse<ConsoleColor>(input, true, out var color))
        {
            Console.ForegroundColor = color;
        }
    }

    private static void CommandBackground(string[] value)
    {
        if (value.Length == 0) return;
        string input = value[0].ToLower();

        if (input == "reset")
        {
            Console.Write("\x1b[49m"); // Reset background
            return;
        }

        if (input.StartsWith("rgb:"))
        {
            var rgb = ParseRgb(input[4..]);
            if (rgb != null)
                Console.Write($"\x1b[48;2;{rgb[0]};{rgb[1]};{rgb[2]}m");
            return;
        }

        if (input.StartsWith("#"))
        {
            var rgb = ParseHex(input);
            if (rgb != null)
                Console.Write($"\x1b[48;2;{rgb[0]};{rgb[1]};{rgb[2]}m");
            return;
        }

        if (int.TryParse(input, out int ansi))
        {
            Console.Write($"\x1b[48;5;{ansi}m");
            return;
        }

        if (Enum.TryParse<ConsoleColor>(input, true, out var color))
        {
            Console.BackgroundColor = color;
        }
    }

    private static void CommandEffect(string[] value)
    {
        if (value.Length == 0) return;
        string effect = value[0].ToLower();

        string ansiCode = effect switch
        {
            "reset" => "0",
            "bold" => "1",
            "dim" => "2",
            "underline" => "4",
            "blink" => "5",
            "reverse" => "7",
            "hidden" => "8",
            _ => ""
        };

        if (ansiCode != "")
        {
            Console.Write($"\x1b[{ansiCode}m");
        }
    }

    private static int[]? ParseRgb(string rgbText)
    {
        var parts = rgbText.Split(',');
        if (parts.Length != 3) return null;
        if (int.TryParse(parts[0], out int r) &&
            int.TryParse(parts[1], out int g) &&
            int.TryParse(parts[2], out int b))
        {
            return new[] { r, g, b };
        }
        return null;
    }

    private static int[]? ParseHex(string hex)
    {
        if (hex.StartsWith("#")) hex = hex[1..];

        if (hex.Length == 6 &&
            int.TryParse(hex.Substring(0, 2), NumberStyles.HexNumber, null, out int r) &&
            int.TryParse(hex.Substring(2, 2), NumberStyles.HexNumber, null, out int g) &&
            int.TryParse(hex.Substring(4, 2), NumberStyles.HexNumber, null, out int b))
        {
            return new[] { r, g, b };
        }

        return null;
    }
}
