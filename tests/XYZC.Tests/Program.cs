using System;
using System.Collections.Generic;
using XYZC;
using XYZC.Tests;

public class Program
{
    private static readonly Dictionary<string, Action> Examples = new()
    {
        { "entry", ExampleEntry.Active },
        { "stage", ExampleStage.Active },
        { "helloworld", ExampleHelloWorld.Active },
        { "scene", ExampleScene.Active },
    };

    public static void Main(string[] args)
    {
        if (args.Length == 0)
        {
            XyzConsole.WriteLine("Please provide an example name.");
            XyzConsole.WriteLine("Available examples: " + string.Join(", ", Examples.Keys));
            return;
        }

        string exampleName = args[0].ToLower();

        if (Examples.TryGetValue(exampleName, out var exampleAction))
        {
            exampleAction.Invoke();
        }
        else
        {
            XyzConsole.WriteLine($"Example '{exampleName}' not found.");
            XyzConsole.WriteLine("Available examples: " + string.Join(", ", Examples.Keys));
        }
    }
}