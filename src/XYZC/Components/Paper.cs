using System.Text;
using XYZC.Core;

namespace XYZC.Components;

public class Paper : ConsoleObject
{
    public string Addon = string.Empty;

    public Paper(int width, int height)
    {
        Width = width;
        Height = height;
    }

    public Paper(){}
    
    public override void Ready(ConsoleScene scene)
    {
        SizeX = Width;
        SizeY = Height;
        base.Ready(scene);
    }

    public override void Draw(ConsoleScene scene, DrawType type = DrawType.Full)
    {
        var paper = CreateSpaceBlock(Width, Height);
        Printer.AreaPrint(Addon + paper, scene.PositionX + X, scene.PositionY + Y, scene.PositionX, scene.Width, scene.PositionY, scene.Height);
        base.Draw(scene, type);
    }
    
    string CreateSpaceBlock(int width, int height)
    {
        var builder = new StringBuilder();
        for (int i = 0; i < height; i++)
        {
            builder.Append(' ', width);
            if (i < height - 1)
                builder.Append('\n');
        }
        return builder.ToString();
    }

}