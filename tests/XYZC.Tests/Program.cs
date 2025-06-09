using System;
using System.Drawing;
using System.Threading;
using XYZC;
using XYZC.Core;
using XYZC.Components;
using XYZC.Components.Commands;

public class Program
{
    public static void Main(string[] args)
    {
        BasicCommands.Import();
        Console.Clear();
        
        // Create main scene and panel
        var scene = new ConsoleScene();
        var panel = new ConsolePanel(); // Using default 50x14 size
        panel.HorizontalAlign = ConsoleAlign.Center;
        panel.VerticalAlign = ConsoleAlign.Center;
        
        // Create the outer box
        var outerBox = new ConsoleBox();
        outerBox.X = 0;
        outerBox.Y = 0;
        
        // Create components with proper spacing
        var titleLabel = new ConsoleLabel("[color #4B92FF]Welcome to [color #7B4CFF]X[color #9B38FF]Y[color #BB24FF]Z[color #DB10FF]C[color #FF00FF]!")
        {
            TextAlign = ConsoleAlign.Center,
            Y = 2,
            Effects = TextEffect.Bold
        };
        
        var subtitleLabel = new ConsoleLabel("[color #7B8795]Modern Console UI Framework")
        {
            TextAlign = ConsoleAlign.Center,
            Y = 4
        };
        
        var featuresLabel = new ConsoleLabel(
            "✨ [color #4B92FF]Rich Text Formatting[color reset]\n" +
            "🎨 [color #9B38FF]Customizable Styles[color reset]\n" +
            "📦 [color #DB10FF]Flexible Layouts[color reset]")
        {
            TextAlign = ConsoleAlign.Center,
            Y = 6
        };
        
        var ctaLabel = new ConsoleLabel("[effect italic][color #7B8795]Press Ctrl+C key to exit...")
        {
            TextAlign = ConsoleAlign.Center,
            Y = ConsolePanel.DEFAULT_HEIGHT - 3
        };
        
        // Add components in the correct order
        panel.Add(outerBox);
        panel.Add(titleLabel);
        panel.Add(subtitleLabel);
        panel.Add(featuresLabel);
        panel.Add(ctaLabel);
        
        panel.PanelScene.Statics.Add(outerBox);

        // Set up and run the stage
        scene.Add(panel);
        var stage = new ConsoleStage(scene);
        
        // Add cleanup handler for Ctrl+C
        Console.CancelKeyPress += (sender, e) => {
            e.Cancel = true;
            Console.CursorVisible = true;
            stage.Disable();
            Environment.Exit(0);
        };
        
        stage.Enable();
        Console.Clear();
    }
}