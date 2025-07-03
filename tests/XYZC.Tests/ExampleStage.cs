using XYZC.Components;
using XYZC.Components.Commands;
using XYZC.Core;

namespace XYZC.Tests;

public class ExampleStage
{
    public static ConsoleScene Scene = new ConsoleScene();
    public static ConsoleStage? Stage;

    public static void Active()
    {
        BasicCommands.Import();
        Stage = new ConsoleStage(Scene);
        Stage.EndInput();

        Scene.Add(new Label("Hello World!"));
        
        Stage.Enable();
    }
}