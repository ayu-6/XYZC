using XYZC.Core;

namespace XYZC.Components.Mini;

public class Entry : Label, IInputLabel
{
    public static readonly string EnterPrefix = "[color cyan]⟩ [color reset]";
    public static readonly string LoadingPrefix = "[color yellow]◐ [color reset]";
    public static readonly string SuccessPrefix = "[color green]✓ ";
    public static readonly string ErrorPrefix = "[color red]✖ ";
    public static readonly string WarningPrefix = "[color yellow]⚠ [color reset]";
    public static readonly string InfoPrefix = "[color blue]ℹ ";
    
    public string Prefix = String.Empty;
    public string Suffix = String.Empty;
    public string Input = String.Empty;
    public string Placeholder = String.Empty;
    
    public bool Writing = false;
    public int MaxLength { get; set; } = 100;
    public int CursorPosition { get; private set; } = 0;
    public bool MaskInput { get; set; } = false;
    public char MaskChar { get; set; } = '*';

    public override void Ready(ConsoleScene scene)
    {
        CursorPosition = Input.Length;

        string displayContent = String.IsNullOrEmpty(Input) ? Placeholder : (MaskInput ? new string(MaskChar, Input.Length) : Input);
        string targetText = $"{Prefix}{displayContent}{Suffix}";

        int more = Text.Length - targetText.Length;
        if (more > 0)
        {
            targetText += new string(' ', more);
        }

        Text = targetText;
        base.Ready(scene);
    }

    public object Value { get; set; }

    public Task<string> Active()
    {
        if (Writing) return Task.FromResult(Input);

        var taskCompletionSource = new TaskCompletionSource<string>();

        OnInputComplete += input => taskCompletionSource.TrySetResult(input);

        Writing = true;
        UpdateDisplay();

        Task.Run(ProcessKeyboardInput);

        return taskCompletionSource.Task;
    }

    private void UpdateDisplay()
    {
        Ready(null);

        int cursorX = X + Prefix.Length + CursorPosition;
        int cursorY = Y;

        try
        {
            if (Console.CursorVisible)
            {
                Console.SetCursorPosition(cursorX, cursorY);
            }
        }
        catch
        {
        }
    }

    private void ProcessKeyboardInput()
    {
        while (Writing)
        {
            ConsoleKeyInfo keyInfo;
            try
            {
                keyInfo = Console.ReadKey(true);
            }
            catch
            {
                Deactive();
                break;
            }

            ProcessKey(keyInfo);
            UpdateDisplay();

            Thread.Sleep(10);
        }
    }

    private void ProcessKey(ConsoleKeyInfo keyInfo)
    {
        switch (keyInfo.Key)
        {
            case ConsoleKey.Enter:
            case ConsoleKey.Escape:
                Deactive();
                break;

            case ConsoleKey.Backspace:
                if (CursorPosition > 0)
                {
                    Input = Input.Remove(CursorPosition - 1, 1);
                    CursorPosition--;
                }
                break;

            case ConsoleKey.Delete:
                if (CursorPosition < Input.Length)
                {
                    Input = Input.Remove(CursorPosition, 1);
                }
                break;

            case ConsoleKey.LeftArrow:
                if (CursorPosition > 0)
                {
                    CursorPosition--;
                }
                break;

            case ConsoleKey.RightArrow:
                if (CursorPosition < Input.Length)
                {
                    CursorPosition++;
                }
                break;

            case ConsoleKey.Home:
                CursorPosition = 0;
                break;

            case ConsoleKey.End:
                CursorPosition = Input.Length;
                break;

            default:
                if (!char.IsControl(keyInfo.KeyChar) && Input.Length < MaxLength)
                {
                    Input = Input.Insert(CursorPosition, keyInfo.KeyChar.ToString());
                    CursorPosition++;
                }
                break;
        }
    }

    public void Deactive()
    {
        Writing = false;
        OnInputComplete?.Invoke(Input);
    }

    public event Action<string> OnInputComplete;

    public override void Draw(ConsoleScene scene, DrawType type)
    {
        if (Writing)
        {
            Ready(scene);
        }

        base.Draw(scene, type);
    }
}