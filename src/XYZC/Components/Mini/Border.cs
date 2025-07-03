using XYZC.Core;

namespace XYZC.Components.Mini;

public class Border : Box
{
    public ConsoleObject Target;
    
    public Border(){}
    public Border(ConsoleObject target)
    {
        Target = target;
    }

    public override void Ready(ConsoleScene scene)
    {
        Width = Target.SizeX + 2;
        Height = Target.SizeY + 2;
        X = Target.X - GetLine(Parts[0]).Length;
        Y = Target.Y - GetLineLength(Parts[0]);
        base.Ready(scene);
    }

    public string GetLine(string text)
    {
        if (text.Contains('\n')) return text.Split('\n')[0];
        else return text;
    }

    public int GetLineLength(string text)
    {
        if (text.Contains('\n')) return text.Split('\n')[0].Length;
        else return 1;
    }
}