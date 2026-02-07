using QCSimulator.Components;

namespace QCSimulator
{
    internal class SimulationResult
    {
        public bool IsValid { get; }
        public QuantumComputer Computer { get; }

        public double FinalHeat { get; }
        public double Computations { get; }
        public double PowerEU { get; }
        public double Amperage { get; }

        public SimulationResult(QuantumComputer computer, double heat, double computations)
        {
            IsValid = !computer.Rack.Components.Any(x => x.HeatLimit <= heat);
            Computer = computer;

            FinalHeat = heat;
            Computations = computations;

            var basePower = (computer.Overclock * computer.Overvolt * (computer.RackCount + 1));
            PowerEU = 131072 * basePower;
            Amperage = basePower / (int)computer.Voltage;
        }
    }
}
