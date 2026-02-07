using QCSimulator.Components;
using System.Collections.Immutable;
using System.Text.Json;

namespace QCSimulator.Data
{
    internal class DataLoader
    {
        public const string ComponentFile = "Components.txt";
        public DataLoader()
        { }

        public ICollection<Component> LoadComponents()
        {
            var path = Path.Combine("Data", ComponentFile);
            if (!File.Exists(path))
            {
                throw new Exception("Components file missing!");
            }

            var components = new List<Component>();
            foreach (var rawLine in File.ReadLines(path))
            {
                var line = rawLine.Trim();

                if (string.IsNullOrEmpty(line) || line.StartsWith("#"))
                    continue;

                // Validate entry length
                string[] parts = line.Split('|', StringSplitOptions.TrimEntries);
                if (parts.Length != 5)
                {
                    LogWarning($"WARNING: Skipping invalid line: {line}");
                    continue;
                }

                // Validate entry parameters.
                string name = parts[0];
                if (!double.TryParse(parts[1], out double computation) ||
                    !double.TryParse(parts[2], out double heatLimit) ||
                    !double.TryParse(parts[3], out double heatConstant) ||
                    !double.TryParse(parts[4], out double coolConstant))
                {
                    LogWarning($"WARNING: Invalid numeric data on line: {line}");
                    continue;
                }

                var component = new Component(name, computation, heatLimit, heatConstant, coolConstant);
                components.Add(component);
            }

            return components;
        }

        private void LogError(string message)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine(message);
            Console.ResetColor();
        }

        private void LogWarning(string message)
        {
            Console.ForegroundColor = ConsoleColor.Gray;
            Console.WriteLine(message);
            Console.ResetColor();
        }
    }
}
