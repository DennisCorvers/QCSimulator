using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QCSimulator.Components
{
    internal class Component
    {
        public string Name { get; }
        public double Computation { get; }
        public double HeatLimit { get; }
        public double HeatConstant { get; }
        public double CoolConstant { get; }

        public Component(string name, double computation, double heatLimit, double heatConstant, double coolConstant)
        {
            Name = name;
            Computation = computation;
            HeatLimit = heatLimit;
            HeatConstant = heatConstant;
            CoolConstant = coolConstant;
        }
    }
}