using System.Collections;
using System.Collections.Generic;

namespace QCSimulator.Components
{
    internal class Rack
    {
        private readonly Component[] m_components;

        public IReadOnlyCollection<Component> Components => m_components;

        public Rack(Component component1, Component component2, Component component3, Component component4)
        {
            m_components = new Component[4] {
                component1,
                component2,
                component3,
                component4
                };
        }

        public Rack(ICollection<Component> components)
        {
            if (components.Count != 4) throw new ArgumentException("Component count needs to be exactly 4");

            m_components = components.ToArray();
        }
    }
}
