namespace XYZC.Core;

[Flags]
public enum TextEffect
{
    None = 0,
    Bold = 1,
    Underline = 2,
    Dim = 4,
    Italic = 8,
    Blink = 16
}

[Flags]
public enum TextAlign
{
    Left = 0,
    Center = 1,
    Right = 2,
    Full = 3
}


public enum ConsoleAlign
{
    Start,
    Center,
    End
}

public enum ConsoleBoard
{
    Default,
    Full,
}

public enum ConsoleOrder
{
    Horizontal,
    Vertical,
}

public enum ConsoleDisplay
{
    None,
    Line,
    Ghost
}