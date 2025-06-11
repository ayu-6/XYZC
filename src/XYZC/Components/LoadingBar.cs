using System.Drawing;
using System.Text;
using XYZC.Core;

namespace XYZC.Components;

public class LoadingBar : ConsoleObject
{
    private const char DefaultFillChar = '█';
    private const char DefaultEmptyChar = '░';

    private float _progress = 0;
    public float Progress
    {
        get => Math.Clamp(_progress, 0, 1);
        set => _progress = Math.Clamp(value, 0, 1);
    }
    public Color? FillColor { get; set; }
    public Color? EmptyColor { get; set; }
    public char FillCharacter { get; set; }
    public char EmptyCharacter { get; set; }
    
    public LoadingBar(int width = 20)
    {
        Width = width;
        Progress = 0f;
        FillCharacter = DefaultFillChar;
        EmptyCharacter = DefaultEmptyChar;
    }

    public override void Ready(ConsoleScene scene)
    {
        SizeX = Width;
        SizeY = 1;
        base.Ready(scene);
    }

    public override void Draw(ConsoleScene scene, DrawType type)
    {
        var builder = new StringBuilder();
        int fillWidth = (int)(Progress * Width);

        // Add fill color if specified
        if (FillColor.HasValue)
            builder.Append($"[color #{ColorToHex(FillColor.Value)}]");
        
        // Add filled portion
        builder.Append(new string(FillCharacter, fillWidth));

        // Add empty color if specified
        if (EmptyColor.HasValue)
            builder.Append($"[color #{ColorToHex(EmptyColor.Value)}]");
        
        // Add empty portion
        builder.Append(new string(EmptyCharacter, Width - fillWidth));

        string output = builder.ToString();
        Printer.AreaPrint(
            output,
            scene.PositionX + X,
            scene.PositionY + Y,
            scene.PositionX,
            scene.PositionX + scene.Width,
            scene.PositionY,
            scene.PositionY + scene.Height
        );

        base.Draw(scene);
    }

    private string ColorToHex(Color color) =>
        $"{color.R:X2}{color.G:X2}{color.B:X2}";
}
