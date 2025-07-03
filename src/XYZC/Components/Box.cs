using XYZC.Core;

namespace XYZC.Components;

public class Box : ConsoleObject
{
    
    public static readonly string[] DefaultParts = new string[] 
    {
        "┌",
        "─",
        "┐",
        "│",
        " ",
        "│",
        "└",
        "─",
        "┘"
    };

    public static readonly string[] DoubleParts = new string[] 
    {
        "╔",
        "═",
        "╗",
        "║",
        " ",
        "║",
        "╚",
        "═",
        "╝"
    };

    public static readonly string[] RoundParts = new string[] 
    {
        "╭",
        "─",
        "╮",
        "│",
        " ",
        "│",
        "╰",
        "─",
        "╯"
    };

    public static readonly string[] HeavyParts = new string[] 
    {
        "┏",
        "━",
        "┓",
        "┃",
        " ",
        "┃",
        "┗",
        "━",
        "┛"
    };

    public static readonly string[] FancyParts = new string[] 
    {
        "╒",
        "═",
        "╕",
        "│",
        " ",
        "│",
        "└",
        "─",
        "┘"
    };

    public static readonly string[] AsciiSimpleParts = new string[] 
    {
        "+",
        "-",
        "+",
        "|",
        " ",
        "|",
        "+",
        "-",
        "+"
    };

    public static readonly string[] AsciiDotsParts = new string[] 
    {
        ".",
        ".",
        ".",
        "|",
        " ",
        "|",
        "'",
        ".",
        "'"
    };

    public static readonly string[] AsciiStarParts = new string[] 
    {
        "*",
        "=",
        "*",
        "|",
        " ",
        "|",
        "*",
        "=",
        "*"
    };

    public static readonly string[] BlockParts = new string[] 
    {
        "█",
        "█",
        "█",
        "█",
        " ",
        "█",
        "█",
        "█",
        "█"
    };

    public static readonly string[] ShadowParts = new string[] 
    {
        "┌",
        "─",
        "┐",
        "│",
        " ",
        "│",
        "└",
        "─",
        "┘▄"
    };

    public string[] Parts = DoubleParts;
    private const int INFINITE_BOUNDS = 500;

    public System.Drawing.Color? BorderColor { get; set; }
    public System.Drawing.Color? FillColor { get; set; }

    public Box()
    {
        Display = ConsoleDisplay.None;
    }
    
    private string ColorToHex(System.Drawing.Color color) =>
        $"{color.R:X2}{color.G:X2}{color.B:X2}";

    public override void Draw(ConsoleScene scene, DrawType type)
    {
        int left = scene.PositionX + X;
        int top = scene.PositionY + Y;
        int width = Width;
        int height = Height;
        
        // Add border color if specified
        string borderColorPrefix = BorderColor.HasValue ? $"[color #{ColorToHex(BorderColor.Value)}]" : "";
        string fillColorPrefix = FillColor.HasValue ? $"[color #{ColorToHex(FillColor.Value)}]" : "";
        string colorReset = (BorderColor.HasValue || FillColor.HasValue) ? "[color reset]" : "";

        // Top row
        Printer.AreaPrint(borderColorPrefix + Parts[0], left, top, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        string topLine = string.Concat(Enumerable.Repeat(Parts[1], width - 2));
        Printer.AreaPrint(borderColorPrefix + topLine, left + 1, top, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        Printer.AreaPrint(borderColorPrefix + Parts[2] + colorReset, left + width - 1, top, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);

        for (int y = 1; y < height - 1; y++)
        {
            Printer.AreaPrint(borderColorPrefix + Parts[3], left, top + y, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
            string fill = string.Concat(Enumerable.Repeat(Parts[4], width - 2));
            Printer.AreaPrint(fillColorPrefix + fill, left + 1, top + y, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
            Printer.AreaPrint(borderColorPrefix + Parts[5] + colorReset, left + width - 1, top + y, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        }

        Printer.AreaPrint(borderColorPrefix + Parts[6], left, top + height - 1, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        string bottomLine = string.Concat(Enumerable.Repeat(Parts[7], width - 2));
        Printer.AreaPrint(borderColorPrefix + bottomLine, left + 1, top + height - 1, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        Printer.AreaPrint(borderColorPrefix + Parts[8] + colorReset, left + width - 1, top + height - 1, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);

        base.Draw(scene, type);
    }
}