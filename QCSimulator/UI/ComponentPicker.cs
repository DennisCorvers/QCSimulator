using QCSimulator.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
            int componentsToSelect = 4;
            int currentIndex = 0;

            for (int pick = 0; pick < componentsToSelect; pick++)
            {
                ConsoleKey key;

                do
                {
                    Console.Clear();
                    Console.WriteLine($"Select component #{pick + 1}:");

                    for (int i = 0; i < m_components.Count; i++)
                    {
                        if (i == currentIndex)
                            Console.ForegroundColor = ConsoleColor.Cyan;
                        else
                            Console.ResetColor();

                        var selectedComponent = m_components.ElementAt(i);
                        Console.WriteLine($"{i + 1}: {selectedComponent.Name} ({selectedComponent.Computation} Computations)");
                    }

                    Console.ResetColor();

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
    }
}
