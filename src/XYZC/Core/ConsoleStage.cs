using System.Diagnostics;

namespace XYZC.Core;

public class ConsoleStage
{
    public ConsoleStage(ConsoleScene scene)
    {
        Scene = scene;
    }
    
    public ConsoleScene Scene;
    public float DeltaTime = 0f;
    public int SleepTime = 0;

    public List<Action> EnableActions = new();
    public List<Action> UpdateActions = new();
    public List<Action> DisableActions = new();

    public bool active = false;

    public void Frame()
    {
        Console.CursorVisible = false;
        Console.Clear();
        Scene.Draw();
        if (Scene.FullWindow()) Scene.Draw();
        else Scene.UpdateDraw();
        Console.WriteLine("\n\n");
    }
    public void Enable()
    {
        active = true;
        Console.CursorVisible = false;

        foreach (var action in EnableActions)
            action.Invoke();

        Console.Clear();
        Scene.Draw();

        Stopwatch stopwatch = Stopwatch.StartNew();
        long lastTicks = stopwatch.ElapsedTicks;
        double tickFrequency = Stopwatch.Frequency;

        while (active)
        {
            long currentTicks = stopwatch.ElapsedTicks;
            DeltaTime = (float)((currentTicks - lastTicks) / tickFrequency);
            lastTicks = currentTicks;

            foreach (var action in UpdateActions)
                action.Invoke();

            if (Scene.FullWindow()) Scene.Draw();
            else Scene.UpdateDraw();
            
            if(SleepTime != 0)
                Thread.Sleep(SleepTime);
        }

        Console.CursorVisible = true;
    }

    public void Disable()
    {
        active = false;

        foreach (var action in DisableActions)
            action.Invoke();
    }
}