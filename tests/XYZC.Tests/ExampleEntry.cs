using System.Drawing;
using XYZC;
using XYZC.Core;
using XYZC.Components;
using XYZC.Components.Commands;
using XYZC.Components.Mini;

namespace XYZC.Tests;

public class ExampleEntry
{
    public static Dictionary<string, Entry> Entries = new Dictionary<string, Entry>();
    public static ConsoleScene scene = new ConsoleScene();
    public static Random Random = new Random();
    public static List<string> Perfixes = new List<string>()
    {
        Entry.SuccessPrefix,
        Entry.ErrorPrefix,
        Entry.WarningPrefix,
        Entry.InfoPrefix
    };
    
    public static void Active()
    {
        BasicCommands.Import();
        
        AddEntry("hobby", "What is your favorite hobby?");
        AddEntry("earlyBird", "Are you an early bird or a night owl?");
        AddEntry("social", "Do you consider yourself introverted or extroverted?");
        AddEntry("calm", "How do you usually handle stress?");
        AddEntry("adventure", "Do you enjoy trying new things?");
        AddEntry("kindness", "What does kindness mean to you?");
        AddEntry("trust", "Do you trust people easily?");
        AddEntry("dream", "What’s a dream you’ve never told anyone?");
        AddEntry("fear", "What’s your biggest fear?");
        AddEntry("values", "What value do you hold above all else?");

        Label label = new Label("inputs...\n there is no values yet...")
        {
            LocalX = 1,
            LocalY = Entries.Count * 3 + 3,
            ForegroundColor = Color.White,
            Margin = 4,
            Height = 16
        };
        scene.Add(label);
        Border border = new Border
        {
            Target = label,
            Parts = Box.RoundParts,
            HorizontalBoard = ConsoleBoard.Full,
            BorderColor = Color.FromArgb(80, 80, 80)
        };
        scene.Static(border);
        
        var stage = new ConsoleStage(scene);
        stage.EnableActions.Add(async () =>
        {
            await Task.Delay(10);
            foreach (var entry in Entries.Values)
            {
                await entry.Active();
                entry.Prefix = Entry.LoadingPrefix;
                await Task.Delay(1);
                entry.Prefix = Perfixes[Random.Next(0, Perfixes.Count - 1)];;
            }
            label.Text = ($"[color gray]The Result:\n");
            foreach (var entryKV in Entries)
            {
                label.Text += ($"[color white]    {entryKV.Key} : {(String.IsNullOrEmpty(entryKV.Value.Input) ? "None" : entryKV.Value.Input)}\n");
            }
            stage.UpdateStatic(border);
        });
        new Thread(stage.Enable).Start();
    }

    public static void AddEntry(string tag, string question)
    {
        Entry entry = new Entry()
        {
            X = 1,
            Y = 1 + Entries.Count * 3,
            Prefix = Entry.EnterPrefix,
            Placeholder = $"[color #606060]{question}[color reset]"
        };
        scene.Add(entry);
        
        Border border = new Border()
        {
            Target = entry,
            Parts = Box.RoundParts,
            HorizontalBoard = ConsoleBoard.Full,
            BorderColor = Color.FromArgb(80, 80, 80)
        };
        scene.Static(border);
        
        Entries.Add(tag, entry);
    }
}