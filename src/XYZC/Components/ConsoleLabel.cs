using XYZC.Core;
using System.Drawing;
using System.Text;

namespace XYZC.Components
{
    public class ConsoleLabel : ConsoleObject
    {
        private const int DEFAULT_PADDING = 2;

        public ConsoleLabel() { }

        public ConsoleLabel(string text)
        {
            Text = text;
            LocalX = DEFAULT_PADDING;
        }

        public string Text { get; set; } = string.Empty;
        public Color? ForegroundColor { get; set; }
        public Color? BackgroundColor { get; set; }
        public TextEffect Effects { get; set; } = TextEffect.None;
        public ConsoleAlign TextAlign { get; set; } = ConsoleAlign.Start;

        private int DefaultWrapWidth => ConsolePanel.DEFAULT_WIDTH - (2 * DEFAULT_PADDING);
        public int? WrapWidth { get; set; }
        public bool SmartWrap { get; set; } = true;

        [Obsolete("Use WrapWidth instead")]
        public int? MaxWidth { 
            get => WrapWidth; 
            set => WrapWidth = value; 
        }

        public override void Draw(ConsoleScene scene, DrawType type)
        {
            string styledText = BuildStyledText(scene);

            int startX = scene.PositionX + X;
            int startY = scene.PositionY + Y;
            
            int effectiveWidth = Math.Min(
                WrapWidth ?? DefaultWrapWidth,
                Math.Min(scene.Width - (2 * DEFAULT_PADDING), Console.WindowWidth - startX)
            );
            
            int effectiveHeight = Math.Min(scene.Height - Y, Console.WindowHeight - startY);

            Printer.AreaPrint(
                styledText,
                startX,
                startY,
                startX,
                startX + effectiveWidth,
                startY,
                startY + effectiveHeight
            );

            base.Draw(scene);
        }

        private string BuildStyledText(ConsoleScene scene)
        {
            var builder = new StringBuilder();

            if (ForegroundColor.HasValue)
                builder.Append($"[color #{ColorToHex(ForegroundColor.Value)}]");

            if (BackgroundColor.HasValue)
                builder.Append($"[background #{ColorToHex(BackgroundColor.Value)}]");

            foreach (var effect in GetEffects())
                builder.Append($"[effect {effect}]");

            var rawLines = Text.Split('\n');
            var wrappedLines = new List<string>();

            // Apply wrapping if WrapWidth is set - breaks long lines into multiple new lines
            if (WrapWidth.HasValue && WrapWidth.Value > 0)
            {
                foreach (var line in rawLines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        wrappedLines.Add(string.Empty);
                        continue;
                    }

                    if (GetTextLength(line) <= WrapWidth.Value)
                    {
                        wrappedLines.Add(line);
                        continue;
                    }

                    wrappedLines.AddRange(WrapText(line, WrapWidth.Value));
                }
            }
            else
            {
                wrappedLines = rawLines.ToList();
            }

            // Calculate effective width for alignment (excluding command tags)
            int maxVisibleLength = wrappedLines.Any() ? 
                wrappedLines.Max(l => GetTextLength(l)) : 0;

            // If WrapWidth is set, use it as the target width
            int targetWidth = WrapWidth ?? maxVisibleLength;

            // Apply text alignment to each line
            foreach (var line in wrappedLines)
            {
                var justified = JustifyLine(line, targetWidth);
                builder.AppendLine(justified);
            }

            return builder.ToString();
        }

        private string JustifyLine(string line, int width)
        {
            // Get visible length (excluding commands)
            int visibleLength = GetTextLength(line);
            int space = width - visibleLength;
            
            if (space <= 0) return line;

            // Split the line into visible text and commands for proper alignment
            var (prefix, mainText, suffix) = ExtractCommands(line);

            string alignedText = TextAlign switch
            {
                ConsoleAlign.Center => prefix + new string(' ', space / 2) + mainText + suffix,
                ConsoleAlign.End => prefix + new string(' ', space) + mainText + suffix,
                ConsoleAlign.Full => prefix + JustifyTextFull(mainText, width) + suffix,
                _ => line // Start alignment, return as is
            };

            return alignedText;
        }

        private (string prefix, string text, string suffix) ExtractCommands(string line)
        {
            var prefix = new StringBuilder();
            var suffix = new StringBuilder();
            var text = new StringBuilder();
            var activeCommands = new Stack<string>();
            bool insideCommand = false;
            var currentCommand = new StringBuilder();

            for (int i = 0; i < line.Length; i++)
            {
                char c = line[i];
                if (c == '[')
                {
                    insideCommand = true;
                    currentCommand.Clear();
                    currentCommand.Append(c);
                }
                else if (insideCommand)
                {
                    currentCommand.Append(c);
                    if (c == ']')
                    {
                        insideCommand = false;
                        string cmd = currentCommand.ToString();
                        
                        if (cmd.Contains("rest]"))
                        {
                            suffix.Append(cmd);
                            if (activeCommands.Count > 0) activeCommands.Pop();
                        }
                        else
                        {
                            prefix.Append(cmd);
                            activeCommands.Push(cmd);
                        }
                    }
                }
                else
                {
                    text.Append(c);
                }
            }

            // Add any remaining active commands to the suffix in reverse order
            foreach (var cmd in activeCommands)
            {
                string restCmd = cmd.Replace("[color", "[color rest")
                                   .Replace("[effect", "[effect rest");
                suffix.Insert(0, restCmd);
            }

            return (prefix.ToString(), text.ToString(), suffix.ToString());
        }

        private string JustifyTextFull(string text, int width)
        {
            if (string.IsNullOrWhiteSpace(text) || !text.Contains(' '))
                return text;

            // Extract commands and get clean text
            var (prefix, mainText, suffix) = ExtractCommands(text);
            
            // Split into words
            string[] words = mainText.Split(' ', StringSplitOptions.RemoveEmptyEntries);
            if (words.Length <= 1) return text;

            // Calculate spaces needed
            int totalWordsLength = words.Sum(w => w.Length);
            int spacesNeeded = width - totalWordsLength;
            int gapCount = words.Length - 1;

            if (gapCount <= 0 || spacesNeeded <= 0)
                return text;

            // Calculate space distribution
            int spacesPerGap = spacesNeeded / gapCount;
            int extraSpaces = spacesNeeded % gapCount;

            // Build justified text
            var result = new StringBuilder();
            result.Append(prefix); // Add opening commands

            for (int i = 0; i < words.Length; i++)
            {
                result.Append(words[i]);

                // If not the last word, add justified spaces
                if (i < words.Length - 1)
                {
                    int spaces = spacesPerGap + (i < extraSpaces ? 1 : 0);
                    result.Append(new string(' ', spaces));
                }
            }

            result.Append(suffix); // Add closing commands
            return result.ToString();
        }

        /// <summary>
        /// Breaks a long text line into multiple shorter lines based on the given width.
        /// The original text is not truncated; instead, it's distributed across multiple lines.
        /// </summary>
        /// <param name="text">The text to wrap</param>
        /// <param name="wrapWidth">The width at which to wrap the text</param>
        /// <returns>A list of text lines, each no longer than the specified width</returns>
        private List<string> WrapText(string text, int wrapWidth)
        {
            var lines = new List<string>();
            var words = text.Split(' ');
            var currentLine = new StringBuilder();

            foreach (var word in words)
            {
                if (currentLine.Length + word.Length + 1 <= wrapWidth)
                {
                    if (currentLine.Length > 0)
                        currentLine.Append(' ');
                    currentLine.Append(word);
                }
                else
                {
                    if (currentLine.Length > 0)
                        lines.Add(currentLine.ToString());
                    
                    // Handle words longer than width
                    if (word.Length > wrapWidth)
                    {
                        int start = 0;
                        while (start < word.Length)
                        {
                            int chunkSize = Math.Min(wrapWidth, word.Length - start);
                            lines.Add(word.Substring(start, chunkSize));
                            start += wrapWidth;
                        }
                        currentLine.Clear();
                    }
                    else
                    {
                        currentLine.Clear();
                        currentLine.Append(word);
                    }
                }
            }

            if (currentLine.Length > 0)
                lines.Add(currentLine.ToString());

            return lines;
        }

        /// <summary>
        /// Gets the effective length of text, excluding command tags
        /// </summary>
        private int GetTextLength(string text)
        {
            int length = 0;
            bool insideCommand = false;

            foreach (char c in text)
            {
                if (c == '[') insideCommand = true;
                else if (c == ']') insideCommand = false;
                else if (!insideCommand) length++;
            }

            return length;
        }

        private IEnumerable<string> GetEffects()
        {
            foreach (TextEffect effect in Enum.GetValues(typeof(TextEffect)))
            {
                if (effect != TextEffect.None && Effects.HasFlag(effect))
                    yield return effect.ToString().ToLower();
            }
        }

        private string ColorToHex(Color color) =>
            $"{color.R:X2}{color.G:X2}{color.B:X2}";

        public override void Ready(ConsoleScene scene)
        {
            var rawLines = Text.Split('\n');
            var wrappedLines = new List<string>();

            // Apply wrapping if WrapWidth is set - breaks long lines into multiple new lines
            if (WrapWidth.HasValue && WrapWidth.Value > 0)
            {
                foreach (var line in rawLines)
                {
                    if (string.IsNullOrEmpty(line))
                    {
                        wrappedLines.Add(string.Empty);
                        continue;
                    }

                    if (line.Length <= WrapWidth.Value)
                    {
                        wrappedLines.Add(line);
                        continue;
                    }

                    wrappedLines.AddRange(WrapText(line, WrapWidth.Value));
                }
            }
            else
            {
                wrappedLines = rawLines.ToList();
            }

            SizeX = wrappedLines.Any() ? wrappedLines.Max(l => Printer.Uncommands(l).Length) : 0;
            SizeY = wrappedLines.Count;
            base.Ready(scene);
        }
    }
}
