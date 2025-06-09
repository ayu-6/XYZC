namespace XYZC.Core;

public class ConsoleObject
{
    public int SizeX, SizeY;
    public int LocalX, LocalY;
    public int X
    {
        get => GetHorizontalAlign() + LocalX;
        set => LocalX = value - GetHorizontalAlign();
    }
    public int Y
    {
        get => GetVerticalAlign() + LocalY;
        set => LocalY = value - GetHorizontalAlign();
    }
    public ConsoleAlign HorizontalAlign = ConsoleAlign.Start;
    public ConsoleAlign VerticalAlign = ConsoleAlign.Start;

    public enum DrawType
    {
        Single,
        Full,
        Update
    }
    
    public virtual void Draw(ConsoleScene scene, DrawType type = DrawType.Full)
    {
        
    }

    public void Draw()
    {
        ConsoleScene.ApplyDefaultScene();
        Draw(ConsoleScene.DefaultScene, DrawType.Single);
        Console.WriteLine();
    } 
    
    private ConsoleScene Scene;
    public virtual void Ready(ConsoleScene scene)
    {
        Scene = scene;
    }

    public int GetHorizontalAlign()
    {
        switch (HorizontalAlign)
        {
            case ConsoleAlign.Start:
                return 0;
            case ConsoleAlign.Center:
                return Scene.Width / 2 - SizeX / 2;
            case ConsoleAlign.End:
                return Scene.Width - 1 - SizeX;
            default:
                return 0;
        }
    }
    public int GetVerticalAlign()
    {
        switch (VerticalAlign)
        {
            case ConsoleAlign.Start:
                return 0;
            case ConsoleAlign.Center:
                return Scene.Height / 2 - SizeY / 2;
            case ConsoleAlign.End:
                return Scene.Height - 1 - SizeY;
            default:
                return 0;
        }
    }
}

public enum ConsoleAlign
{
    Start,
    Center,
    End,
    Full
}