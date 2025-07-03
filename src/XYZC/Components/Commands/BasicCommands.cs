using XYZC.Core;
using System.Globalization;

namespace XYZC.Components.Commands;

public class BasicCommands
{
    public static void Import()
    {
        Commander.AddCommand("add", CommandAddVariable);
        Commander.AddCommand("remove", CommandRemoveVariable);
        Commander.AddCommand("set", CommandSetVariable);
        Commander.AddCommand("color", CommandColor);
        Commander.AddCommand("background", CommandBackground);
        Commander.AddCommand("effect", CommandEffect);
    }

    private static void CommandAddVariable(string[] value)
    {
        if (value.Length < 2) return;
        Commander.Variables.Add(value[0], value[1]);
    }
    
    private static void CommandRemoveVariable(string[] value)
    {
        if (value.Length < 1) return;
        Commander.Variables.Remove(value[0]);
    }

    private static void CommandSetVariable(string[] value)
    {
        if (value.Length < 2) return;
        Commander.Variables[value[0]] = value[1];
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
        string input = value[0].ToLower();

        if (input == "reset")
        {
            Console.Write("\x1b[0m"); // Reset all effects
            return;
        }

        switch (input)
        {
            case "bold":
                Console.Write("\x1b[1m");
                break;
            case "dim":
                Console.Write("\x1b[2m");
                break;
            case "italic":
                Console.Write("\x1b[3m");
                break;
            case "underline":
                Console.Write("\x1b[4m");
                break;
            case "blink":
                Console.Write("\x1b[5m");
                break;
            case "rest":
                Console.Write("\x1b[0m"); // Reset all effects for "rest" command
                break;
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
