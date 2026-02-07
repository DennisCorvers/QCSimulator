using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace QCSimulator.Components
{
    internal class QuantumComputer
    {
        private readonly static Random m_random = new();

        private readonly Rack m_rack;
        private readonly double m_overclock;
        private readonly double m_overvolt;
        private readonly int m_rackCount;
        private readonly Voltage m_voltage;

        public int RackCount => m_rackCount;
        public double Overvolt => m_overvolt;
        public double Overclock => m_overclock;
        internal Voltage Voltage => m_voltage;
        internal Rack Rack => m_rack;

        public QuantumComputer(Rack rack, double overclock, double overvolt, int rackCount, Voltage voltage)
        {
            m_rack = rack;
            m_overclock = overclock;
            m_overvolt = overvolt;
            m_rackCount = rackCount;
            m_voltage = voltage;
        }

        public SimulationResult Simulate()
        {
            var stats = new SimulationStats();
            double oldHeat = 0;
            double newHeat = 10000;

            while (Math.Abs(newHeat - oldHeat) > 0)
            {
                oldHeat = newHeat;

                var (computations, heat) = GetComputation(stats.Heat);
                var tempHeat = heat;
                stats.AddSample(computations);

                stats.Heat = GetHeat(tempHeat);

                newHeat = stats.Heat;
            }

            var avgComps = stats.ComputationSamples.Average() * RackCount;
            return new SimulationResult(this, newHeat, avgComps);
        }

        private (double computations, double heat) GetComputation(double computerHeat)
        {
            double rackHeat = 0;
            double computation = 0;

            foreach (var component in m_rack.Components)
            {
                if (computerHeat >= 0)
                {
                    double h = component.HeatConstant > 0
                        ? component.HeatConstant * m_overclock * Math.Pow(m_overvolt, 2)
                        : -10;

                    rackHeat += h * (1 + component.CoolConstant * computerHeat / 100000);

                    if (m_overvolt > m_random.NextDouble())
                    {
                        computation += component.Computation
                            * (1 + Math.Pow(m_overclock, 2))
                            / (1 + Math.Pow(m_overclock - m_overvolt, 2));
                    }
                }
            }

            var newHeat = computerHeat + Math.Ceiling(rackHeat);
            return (Math.Floor(computation), newHeat);
        }

        private double GetHeat(double computerHeat)
        {
            if (computerHeat > 0)
            {
                var heatC = m_rack.Components
                    .Where(x => x.HeatConstant < 0)
                    .Sum(x => x.HeatConstant * (computerHeat / 10000));

                computerHeat += Math.Max(-computerHeat, Math.Ceiling(heatC));
                computerHeat -= Math.Max((int)computerHeat / 1000, 20);
            }
            else if (computerHeat < 0)
            {
                computerHeat -= Math.Min((int)computerHeat / 1000, -1);
            }

            return computerHeat;
        }

        private class SimulationStats
        {
            private List<double> m_computations;

            public IReadOnlyList<double> ComputationSamples
                => m_computations;

            public double Heat { get; set; }

            public SimulationStats()
            {
                m_computations = new List<double>();
            }

            public void AddSample(double computations) => m_computations.Add(computations);
        }
    }
}
