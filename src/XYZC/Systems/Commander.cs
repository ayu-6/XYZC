namespace XYZC.Core
{
    public class Commander
    {
        public static Dictionary<string, Action<string[]>> Commands = new Dictionary<string, Action<string[]>>();
        public static Dictionary<string, string> Variables = new Dictionary<string, string>();

        public static void Execute(string command)
        {
            if (string.IsNullOrWhiteSpace(command))
                return;

            var parts = command.Split(' ', StringSplitOptions.RemoveEmptyEntries).ToList();
            var key = parts[0].ToLower();
            parts.RemoveAt(0);

            for (int i = 0; i < parts.Count; i++)
            {
                var (result, wrapped) = ParseBracedString(parts[i]);
                if (wrapped)
                    parts[i] = Variables[result];
            }

            if (Commands.TryGetValue(key, out var action))
            {
                action(parts.ToArray());
            }
            else
            {
                throw new KeyNotFoundException($"Command '{key}' not found.");
            }
        }

        public static (string result, bool wrapped) ParseBracedString(string input)
        {
            if (string.IsNullOrEmpty(input))
                return (input, false);

            int startCount = 0;
            while (startCount < input.Length && input[startCount] == '{')
                startCount++;

            int endCount = 0;
            while (endCount < input.Length && input[input.Length - 1 - endCount] == '}')
                endCount++;

            var cleaned = input.Replace("{{", "{").Replace("}}", "}");

            if (cleaned.Length >= 2
                && cleaned.StartsWith("{")
                && cleaned.EndsWith("}")
                && !(startCount == 2 && endCount == 2))
            {
                var inner = cleaned.Substring(1, cleaned.Length - 2);
                return (inner, true);
            }

            return (cleaned, false);
        }

        public static void AddCommand(string key, Action<string[]> command)
        {
            if (string.IsNullOrWhiteSpace(key) || command == null)
                throw new ArgumentException("Key and command must be provided.");

            Commands[key.ToLower()] = command;
        }
    }
}
