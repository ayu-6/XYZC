using XYZC.Core;

namespace XYZC.Components;

public class ConsolePanel : ConsoleObject
{
    public const int DEFAULT_WIDTH = 50;
    public const int DEFAULT_HEIGHT = 14;
    public const int DEFAULT_PADDING = 2;

    public ConsoleScene PanelScene;

    public ConsolePanel()
    {
        PanelScene = new ConsoleScene(DEFAULT_WIDTH, DEFAULT_HEIGHT);
        SizeX = DEFAULT_WIDTH;
        SizeY = DEFAULT_HEIGHT;
    }

    public override void Ready(ConsoleScene scene)
    {
        base.Ready(scene);
    }

    public override void Draw(ConsoleScene scene, DrawType type)
    {
        PanelScene.PositionX = scene.PositionX + X;
        PanelScene.PositionY = scene.PositionY + Y;
        
        if(type == DrawType.Update) 
        {
            PanelScene.UpdateDraw();
        }
        else 
        {
            PanelScene.Draw(false);
        }
        
        base.Draw(scene, type);
    }

    public void Add(ConsoleObject obj)
    {
        obj.LocalX += DEFAULT_PADDING; // Apply default padding
        PanelScene.Add(obj);
    }
}