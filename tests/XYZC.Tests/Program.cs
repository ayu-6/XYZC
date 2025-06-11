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

        var scene = new ConsoleScene();
        var outerBox = new Box();
        
        var titleLabel = new AnimatedLabel()
        {
            Texts = new List<string>()
            {
                "[color #FF4B4B]Welcome to [color #FF4BFF]XYZC!",
                "[color #FF774B]Welcome to [color #D14BFF]XYZC!",
                "[color #FFA24B]Welcome to [color #A64BFF]XYZC!",
                "[color #FFCD4B]Welcome to [color #7B4BFF]XYZC!",
                "[color #E0FF4B]Welcome to [color #4B5FFF]XYZC!",
                "[color #B5FF4B]Welcome to [color #4B92FF]XYZC!",
                "[color #8AFF4B]Welcome to [color #4BC5FF]XYZC!",
                "[color #5FFF4B]Welcome to [color #4BFFDB]XYZC!",
                "[color #4BFF70]Welcome to [color #4BFF9B]XYZC!",
                "[color #4BFF9B]Welcome to [color #4BFF70]XYZC!",
                "[color #4BFFDB]Welcome to [color #5FFF4B]XYZC!",
                "[color #4BC5FF]Welcome to [color #8AFF4B]XYZC!",
                "[color #4B92FF]Welcome to [color #B5FF4B]XYZC!",
                "[color #4B5FFF]Welcome to [color #E0FF4B]XYZC!",
                "[color #7B4BFF]Welcome to [color #FFCD4B]XYZC!",
                "[color #A64BFF]Welcome to [color #FFA24B]XYZC!",
                "[color #D14BFF]Welcome to [color #FF774B]XYZC!",
                "[color #FF4BFF]Welcome to [color #FF4B4B]XYZC!",
            },
            HorizontalAlign = ConsoleAlign.Center,
            TextAlign = TextAlign.Left,
            Effects = TextEffect.Bold
        };
        titleLabel.Play();
        
        var subtitleLabel = new Label("[color #7B8795]Modern Console UI Framework")
        {
            HorizontalAlign = ConsoleAlign.Center,
            TextAlign = TextAlign.Center,
        };

        var featuresLabel = new Label(
            "✨ [color #4B92FF]Rich Text Formatting[color reset]\n" +
            "🎨 [color #9B38FF]Customizable Styles[color reset]\n" +
            "📦 [color #DB10FF]Flexible Layouts[color reset]\n" +
            "⚡ [color #FF00FF]Loading Bars[color reset]")
        {
            TextAlign = TextAlign.Left,
        };
        
        var ctaLabel = new Label("[effect italic][color #7B8795]Press Ctrl+C key to exit...")
        {
            TextAlign = TextAlign.Center,
            HorizontalAlign = ConsoleAlign.Center,
            VerticalAlign = ConsoleAlign.End
        };
        
        scene.Add(titleLabel);
        scene.Add(subtitleLabel);
        scene.Add(featuresLabel);
        scene.Add(ctaLabel);
        scene.Add(outerBox);
        
        scene.Statics.Add(outerBox);
        
        var stage = new ConsoleStage(scene);
        
        stage.Enable();
    }
}