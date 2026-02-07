using QCSimulator.Components;
using System;
using System.Collections.Generic;
using System.Linq;

namespace QCSimulator.UI
{
    internal class ComponentPicker
    {
        private int m_count;
        private ICollection<Component> m_components;
        private IDictionary<string, Component> m_componentsMap;

        public ComponentPicker(int count, ICollection<Component> components)
        {
            m_count = count;
            m_components = components;
            m_componentsMap = components.ToDictionary(k => k.Name, v => v);
        }

        public ICollection<Component> Pick()
        {
            var selectedComponents = new List<Component>();
            int componentsToSelect = m_count;
            int currentIndex = 0;

            for (int pick = 0; pick < componentsToSelect; pick++)
            {
                // Draw menu once
                Console.Clear();
                Console.WriteLine($"Select component #{pick + 1}:");
                int startRow = Console.CursorTop;

                // Print all component names
                for (int i = 0; i < m_components.Count; i++)
                {
                    var c = m_components.ElementAt(i);
                    Console.WriteLine($"{i + 1}: {c.Name} ({c.Computation} Computations)");
                }

                ConsoleKey key;
                do
                {
                    // Highlight current line only
                    for (int i = 0; i < m_components.Count; i++)
                    {
                        Console.SetCursorPosition(0, startRow + i);

                        var c = m_components.ElementAt(i);

                        if (i == currentIndex)
                        {
                            Console.BackgroundColor = ConsoleColor.White;
                            Console.ForegroundColor = ConsoleColor.Black;
                        }
                        else
                        {
                            Console.ResetColor();
                        }

                        // Clear the line first
                        Console.Write(new string(' ', Console.WindowWidth));
                        Console.SetCursorPosition(0, startRow + i);

                        // Write the component
                        Console.Write($"{i + 1}: {c.Name} {GetComponentInfo(c)}");
                    }

                    Console.ResetColor();

                    // Read key
                    var keyInfo = Console.ReadKey(intercept: true);
                    key = keyInfo.Key;

                    if (key == ConsoleKey.UpArrow)
                    {
                        currentIndex = currentIndex == 0 ? m_components.Count - 1 : currentIndex - 1;
                    }
                    else if (key == ConsoleKey.DownArrow)
                    {
                        currentIndex = (currentIndex + 1) % m_components.Count;
                    }

                } while (key != ConsoleKey.Enter);

                // Add selected component
                selectedComponents.Add(m_components.ElementAt(currentIndex));
            }

            return selectedComponents;
        }

        private static string GetComponentInfo(Component component)
        {
            return string.Empty;
        }
    }
}
