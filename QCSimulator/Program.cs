using QCSimulator;
using QCSimulator.Components;
using QCSimulator.Data;
using QCSimulator.UI;

// Data loading
var data = (new DataLoader()).LoadComponents();

Console.WriteLine("Configure your simulation parameters:\n");
Console.WriteLine();

// Variable inputs
var clock = ConsoleParser.ReadDouble("Enter overclock multiplier", 1.00);
var voltage = ConsoleParser.ReadDouble("Enter overvolt multiplier", 1.00);
var racks = (int)ConsoleParser.ReadDouble("Enter number of racks", 2);

var voltageTier = ConsoleParser.ReadEnum("Select voltage tier:", Voltage.ZPM);

// Component inputs
var picker = new ComponentPicker(4, data);
var components = picker.Pick();

var rack = new Rack(components);
var computer = new QuantumComputer(rack, clock, voltage, racks, voltageTier);

Console.Clear();
WriteAffirmation("Simulating your computer:");
Console.WriteLine(string.Join(", ", components.Select(x=>x.Name)));
Console.WriteLine();

var result = computer.Simulate();

if (!result.IsValid)
{
    var failedComponent = computer.Rack.Components.First(x => x.HeatLimit <= result.FinalHeat);
    WriteError($"VOID: The heat will exceed the limit for {failedComponent.Name} ({failedComponent.HeatLimit:N0}).");
    WriteMessage($"Final Heat Approximation: {(int)result.FinalHeat:N0}");
}
else
{
    WriteAffirmation("SAFE: The heat will NOT exceed the limit for any component.");
    WriteMessage($"Final Heat Approximation: {result.FinalHeat:N0}");
    WriteMessage($"Average Computation: {result.Computations:N0}/s");
    WriteMessage($"Total Power: {result.PowerEU:N0} EU/t ({result.Amperage:0.00}A {result.Computer.Voltage})");
}

Console.Read();


static void WriteError(string text)
{
    Console.ForegroundColor = ConsoleColor.Red;
    Console.WriteLine(text);
    Console.ResetColor();
}

static void WriteAffirmation(string text)
{
    Console.ForegroundColor = ConsoleColor.Green;
    Console.WriteLine(text);
    Console.ResetColor();
}

static void WriteMessage(string text)
{
    Console.WriteLine(text);
}

