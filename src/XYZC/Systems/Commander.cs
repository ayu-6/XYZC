namespace XYZC.Core;

public class Commander
{
    public static Dictionary<string, Action<string[]>> Commands = new Dictionary<string, Action<string[]>>();

    public static void Execute(string command)
    {
        List<string> parts = command.Split(' ').ToList();
        string key =  parts[0];
        parts.RemoveAt(0);
        Commands[key.ToLower()].Invoke(parts.ToArray());
    }

    public static void AddCommand(string key, Action<string[]> command) => Commands.Add(key, command);
}