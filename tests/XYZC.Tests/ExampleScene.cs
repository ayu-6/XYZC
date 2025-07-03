using XYZC.Components;
using XYZC.Components.Commands;
using XYZC.Core;

namespace XYZC.Tests;

public class ExampleScene
{
    public static ConsoleScene Scene = new ConsoleScene();
    
    public static void Active()
    {
        BasicCommands.Import();

        Scene.Add(new Label("Hello World!"));
        
        Scene.Draw();
    }
}