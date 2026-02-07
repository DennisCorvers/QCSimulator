namespace QCSimulator.UI
{
    internal static class ConsoleParser
    {
        public static double ReadDouble(string message, double defaultValue)
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(message);
            Console.ResetColor();
            Console.Write($" (default {defaultValue}): ");
            string? input = Console.ReadLine();

            if (double.TryParse(input, out double value))
                return value;

            return defaultValue;
        }

        public static T ReadEnum<T>(string message, T defaultValue) where T : struct, Enum
        {
            var values = Enum.GetValues<T>();
            Console.ForegroundColor = ConsoleColor.Green;
            Console.WriteLine(message);
            Console.ResetColor();

            for (int i = 0; i < values.Length; i++)
            {
                Console.WriteLine($"{i + 1}: {values[i]}");
            }

            int choice;
            while (!int.TryParse(Console.ReadLine(), out choice) || choice < 1 || choice > values.Length)
            {
                Console.WriteLine(message);
            }

            return values[choice - 1];
        }
    }
}
