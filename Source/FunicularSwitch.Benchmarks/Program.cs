// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FunicularSwitch.Benchmarks;

var summary = BenchmarkRunner.Run<GenResBenchmark>();

Console.WriteLine(summary);