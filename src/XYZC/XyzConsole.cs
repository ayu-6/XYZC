using XYZC.Core;

namespace XYZC;

public class XyzConsole
{
    public static void Write(params object[] values)
    {
        foreach (var value in values)
        {
            Printer.Print(value.ToString(), false, 0, Int32.MaxValue, 0, Int32.MaxValue);
        }
    }

    public static void WriteLine(params object[] values)
    {
        Write(values);
        Console.WriteLine();
    }
    
    public static void WriteArea(string text, int x, int y)
    {
        Printer.AreaPrint(text, x, y, 0, Console.WindowWidth, 0, Console.WindowHeight);
    }
}

