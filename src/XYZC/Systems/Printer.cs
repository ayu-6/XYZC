namespace XYZC.Core;

public class Printer
{
    public static void Print(string text, bool newLine = true,
        int? limitStartX = null, int? limitEndX = null,
        int? limitStartY = null, int? limitEndY = null)
    {
        ConsoleColor foregroundColor = Console.ForegroundColor;
        ConsoleColor backgroundColor = Console.BackgroundColor;
        string[] parts = Split(text);
        bool function = false;

        int startX = limitStartX ?? 0;
        int endX = limitEndX ?? Console.WindowWidth;
        int startY = limitStartY ?? 0;
        int endY = limitEndY ?? Console.WindowHeight;

        int x = Console.CursorLeft;
        int y = Console.CursorTop;

        foreach (var part in parts)
        {
            if (function)
            {
                Commander.Execute(part);
            }
            else
            {
                string output = part;

                if (y < startY || y >= endY)
                {
                    if (newLine) { y++; x = startX; }
                    continue;
                }

                int allowed = endX - x;
                if (allowed <= 0)
                {
                    if (newLine) { y++; x = startX; }
                    continue;
                }

                if (output.Length > allowed)
                    output = output[..allowed];

                Console.Write(output);
                x += output.Length;

                if (newLine)
                {
                    Console.WriteLine();
                    x = startX;
                    y++;
                }
            }

            function = !function;
        }

        Console.ForegroundColor = foregroundColor;
        Console.BackgroundColor = backgroundColor;
    }

    public static void AreaPrint(string text, int x, int y, int minWidth, int maxWidth, int minHeight, int maxHeight)
    {
        string[] lines = text.Split("\n");

        int maxX = Console.BufferWidth - 1;
        int maxY = Console.BufferHeight - 1;

        foreach (var line in lines)
        {
            int safeX = Math.Clamp(x, 0, maxX);
            int safeY = Math.Clamp(y, 0, maxY);

            Console.SetCursorPosition(safeX, safeY);
            Print(line, false, minWidth, maxWidth, minHeight, maxHeight);
            y++;
        }
    }

    
    public static string[] Split(string text)
    {
        List<string> result = new List<string>()
        {
            ""
        };

        bool function = false;
        bool comment = false;
        foreach (var part in text)
        {
            if (function)
            {
                if (part == ']')
                {
                    function = false;
                    result.Add("");
                }
                else
                {
                    result[^1] += part;
                }
            }
            else
            {
                if (comment)
                {
                    if(part == '/')
                    {
                        result[^1] += "/";
                    }
                    else
                    {
                        result[^1] += part;
                    }
                    comment = false;
                }
                else
                {
                    if(part == '/') comment = true;
                    else
                    {
                        if (part == '[')
                        {
                            function = true;
                            result.Add("");
                        }
                        else
                        {
                            result[^1] += part;
                        }
                    }
                }
            }
        }
        
        return result.ToArray();
    }
    
    public static string Uncommands(string text)
    {
        var parts = Split(text);
        string result = "";
        bool function = false;
        foreach (var part in parts)
        {
            if(!function) result += part;
            function = !function;
        }
        return result;
    }
}