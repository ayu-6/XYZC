using System;
using System.Collections.Generic;

namespace XYZC.Core;

public class ConsoleScene : Dictionary<string, ConsoleObject>
{
    public static ConsoleScene DefaultScene = new ConsoleScene();
    public static void ApplyDefaultScene()
    {
        DefaultScene.PositionX = Console.CursorLeft;
        DefaultScene.PositionY = Console.CursorTop;
        DefaultScene.Width = Console.WindowWidth;
        DefaultScene.Height = Console.WindowHeight;
    }
    
    public List<ConsoleObject> Statics = new List<ConsoleObject>();
    
    public int LineHeight = 1;
    
    public int Width, Height;

    public int LocalPositionX, LocalPositionY;

    public int PositionX
    {
        get => LocalPositionX;
        set => LocalPositionX = value;
    }

    public int PositionY
    {
        get => LocalPositionY;
        set => LocalPositionY = value;
    }
    
    private static readonly Random _random = new();

    public ConsoleScene(bool fullWindow = true)
    {
        if(fullWindow) FullWindow();
    }
    public ConsoleScene(int width, int height)
    {
        Width = width;
        Height = height;
    }
    public ConsoleScene(int width, int height, int positionX, int positionY)
    {
        Width = width;
        Height = height;
    }

    public bool FullWindow()
    {
        bool result = Width != Console.WindowWidth || Height != Console.WindowHeight;
        Width = Console.WindowWidth;
        Height = Console.WindowHeight;
        return result;
    }

    public string Add(ConsoleObject obj)
    {
        string key;
        do
        {
            key = GenerateKey();
        } while (this.ContainsKey(key));

        this[key] = obj;
        return key;
    }

    public string Static(ConsoleObject obj)
    {
        Statics.Add(obj);
        return Add(obj);
    }
    
    private string GenerateKey()
    {
        const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        var suffix = new char[6];
        for (int i = 0; i < suffix.Length; i++)
            suffix[i] = chars[_random.Next(chars.Length)];

        return "_" + new string(suffix);
    }
    
    public void Draw(bool Clear = true)
    {
        if(Clear) Console.Clear();
        int number = 0;
        foreach (var obj in this.Values)
        {
            if(obj.Display != ConsoleDisplay.None) obj.LocalY += number * LineHeight;
            obj.Ready(this);
        }
        foreach (var obj in this.Values)
        {
            obj.Draw(this, ConsoleObject.DrawType.Full);
            if(obj.Display != ConsoleDisplay.None) obj.LocalY -= number * LineHeight;
            if(obj.Display == ConsoleDisplay.Line) number++;
        }
    }
    
    public void UpdateDraw()
    {
        int number = 0;
        foreach (var obj in this.Values)
        {
            if (!Statics.Contains(obj))
            {
                if(obj.Display != ConsoleDisplay.None) obj.LocalY += number * LineHeight;
                obj.Ready(this);
                obj.Draw(this, ConsoleObject.DrawType.Full);
                if(obj.Display != ConsoleDisplay.None) obj.LocalY -= number * LineHeight;
            }
            if(obj.Display == ConsoleDisplay.Line) number++;
        }
    }
}