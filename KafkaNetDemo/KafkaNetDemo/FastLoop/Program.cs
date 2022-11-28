using BenchmarkDotNet.Running;

// See https://aka.ms/new-console-template for more information

var summary = BenchmarkRunner.Run(typeof(Program).Assembly);

Console.ReadLine();