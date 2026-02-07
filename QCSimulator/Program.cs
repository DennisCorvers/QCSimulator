// See https://aka.ms/new-console-template for more information

using QCSimulator.Components;
using System.Runtime.CompilerServices;

var bioprocessor = new Component("Bioprocessor", 200, 6000, 36, -1);
var coolingCore = new Component("Cooling Core", 0, 10000, -1, 200);

var rack = new Rack(bioprocessor, bioprocessor, coolingCore, coolingCore);
var computer = new QuantumComputer(rack, 1.44, 1.03, 2, QCSimulator.Voltage.UV);

var result = computer.Simulate();

Console.WriteLine("Simulating...");


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