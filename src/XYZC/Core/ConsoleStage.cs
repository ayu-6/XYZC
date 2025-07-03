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

    public void UpdateStatic(ConsoleObject obj)
    {
        Scene.Statics.Remove(obj);
        bool relook = false;
        void Upd()
        {
            if (relook)
            {
                UpdateActions.Remove(Upd);
                Scene.Statics.Add(obj);
            }
            else relook = true;
        }
        UpdateActions.Add(Upd);
    }

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

            var copyUpdateActions = UpdateActions.ToList();
            foreach (var action in copyUpdateActions)
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

    public bool InputWork = true;
    public void EndInput()
    {
        InputWork = false;
        new Thread(() =>
        {
            while (!InputWork)
            {
                Console.ReadKey(true);
            }
        }).Start();
    }
    public void StartInput()
    {
        InputWork = true;
    }
}