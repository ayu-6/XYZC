namespace XYZC.Core;

public class ConsoleObject
{
    public int SizeX, SizeY;
    public int LocalX, LocalY;
    public int LocalWidth, LocalHeight;
    public int Margin = 0;

    public int Width
    {
        get => (HorizontalBoard == ConsoleBoard.Full ? Scene.Width : LocalWidth) - Margin;
        set => LocalWidth = value;
    }

    public int Height
    {
        get => VerticalBoard == ConsoleBoard.Full ? Scene.Height : LocalHeight;
        set => LocalHeight = value;
    }
    private ConsoleScene Scene;

    public ConsoleObject()
    {
        // Initialize with default scene
        Scene = ConsoleScene.DefaultScene ?? new ConsoleScene();
    }

    public int X
    {
        get => GetHorizontalAlign() + LocalX + Margin / 2;
        set => LocalX = value - GetHorizontalAlign();
    }
    
    public int Y
    {
        get => GetVerticalAlign() + LocalY;
        set => LocalY = value - GetVerticalAlign();
    }
    
    public ConsoleAlign HorizontalAlign = ConsoleAlign.Start;
    public ConsoleAlign VerticalAlign = ConsoleAlign.Start;
    public ConsoleBoard HorizontalBoard = ConsoleBoard.Default;
    public ConsoleBoard VerticalBoard = ConsoleBoard.Default;
    public ConsoleDisplay Display = ConsoleDisplay.Line;

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
        if(Display == ConsoleDisplay.Line) Console.WriteLine();
    } 
    
    public virtual void Ready(ConsoleScene scene)
    {
        Scene = scene ?? ConsoleScene.DefaultScene ?? new ConsoleScene();
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
