using XYZC.Core;

namespace XYZC.Components;

public class ConsoleBox : ConsoleObject
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

    public ConsoleBox()
    {
        SizeX = ConsolePanel.DEFAULT_WIDTH;
        SizeY = ConsolePanel.DEFAULT_HEIGHT;
    }

    public override void Draw(ConsoleScene scene, DrawType type)
    {
        int left = scene.PositionX + X;
        int top = scene.PositionY + Y;
        int width = SizeX > 0 ? SizeX : scene.Width;
        int height = SizeY > 0 ? SizeY : scene.Height;

        Printer.AreaPrint(Parts[0], left, top, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        string topLine = string.Concat(Enumerable.Repeat(Parts[1], width - 2));
        Printer.AreaPrint(topLine, left + 1, top, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        Printer.AreaPrint(Parts[2], left + width - 1, top, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);

        for (int y = 1; y < height - 1; y++)
        {
            Printer.AreaPrint(Parts[3], left, top + y, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
            string fill = string.Concat(Enumerable.Repeat(Parts[4], width - 2));
            Printer.AreaPrint(fill, left + 1, top + y, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
            Printer.AreaPrint(Parts[5], left + width - 1, top + y, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        }

        Printer.AreaPrint(Parts[6], left, top + height - 1, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        string bottomLine = string.Concat(Enumerable.Repeat(Parts[7], width - 2));
        Printer.AreaPrint(bottomLine, left + 1, top + height - 1, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);
        Printer.AreaPrint(Parts[8], left + width - 1, top + height - 1, -INFINITE_BOUNDS, INFINITE_BOUNDS, -INFINITE_BOUNDS, INFINITE_BOUNDS);

        base.Draw(scene, type);
    }
}