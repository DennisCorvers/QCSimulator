// See https://aka.ms/new-console-template for more information

using QCSimulator.Components;

var bioprocessor = new Component("Bioprocessor", 200, 6000, 36, -1);
var coolingCore = new Component("Cooling Core", 0, 10000, -1, 200);

var rack = new Rack(bioprocessor, bioprocessor, bioprocessor, coolingCore);
var computer = new QuantumComputer(rack, 1.44, 1.03, 2, QCSimulator.Voltage.UV);

var result = computer.Simulate();

Console.WriteLine("Hello, World!");


