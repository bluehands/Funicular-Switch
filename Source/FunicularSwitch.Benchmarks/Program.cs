// See https://aka.ms/new-console-template for more information

using BenchmarkDotNet.Running;
using FunicularSwitch.Benchmarks;

BenchmarkRunner.Run<GenResBenchmark>();
//BenchmarkRunner.Run<ResBenchmark>();