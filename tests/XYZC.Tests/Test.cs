using XYZC.Components;
using XYZC.Components.Commands;
using XYZC.Core;

namespace XYZC.Tests;

public class Test
{
    public static void Active()
    {
        BasicCommands.Import();
        
        XyzConsole.WriteLine("[add main_color red]");
        XyzConsole.WriteLine("[color {main_color}]Hello, World!");
    }
}