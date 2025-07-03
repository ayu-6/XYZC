using XYZC.Core;

namespace XYZC.Components.Mini;

public class AnimatedLabel : Label
{
    public List<string> Texts = new List<string>();
    public int CurrentTextIndex = 0;
    
    public bool Loop = true;
    public bool Reverse = false;
    public bool Active { get; private set; }
    public int DelayMilliseconds { get; set; } = 60;
        
    public override void Ready(ConsoleScene scene)
    {
        Text = Texts[CurrentTextIndex];
        
        base.Ready(scene);
    }

    public async Task Play()
    {
        Active = true;
        while (Active)
        {
            if (CurrentTextIndex == Texts.Count - 1)
            {
                CurrentTextIndex = 0;
                if(!Loop) Stop();
            }
            else CurrentTextIndex++;

            Text = Texts[CurrentTextIndex];
            await Task.Delay(DelayMilliseconds);
        }

        if (Reverse) Texts.Reverse();
    }

    public void Stop()
    {
        Active = false;
    }
}