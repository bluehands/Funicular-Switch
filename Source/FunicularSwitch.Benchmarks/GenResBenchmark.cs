using BenchmarkDotNet.Attributes;
using FunicularSwitch.Generic;

namespace FunicularSwitch.Benchmarks;

[MemoryDiagnoser()]
public class GenResBenchmark
{
    private GenRes<int, int> _okRes;
    private GenRes<int, int> _errorRes;
    private Func<int, int> _identity;
    private Func<int, GenRes<int, int>> _monadicIdentity;

    [GlobalSetup]
    public void Setup()
    {
        _identity = static i => i;
        _monadicIdentity = GenRes<int, int>.Ok;
        _okRes = GenRes<int, int>.Ok(42);
        _errorRes = GenRes<int, int>.Error(40);
    }

    [Benchmark]
    public GenRes<int, int> CreateOkWithType()
    {
        return GenRes<int, int>.Ok(42);
    }

    [Benchmark]
    public GenRes<int, int> CreateErrorWithType()
    {
        return GenRes<int, int>.Error(40);
        
        
        
    }
    
    [Benchmark]
    public GenRes<int, int> CreateOkWithImplicitOperator()
    {
        return GenRes.Ok(42);
    }
    
    [Benchmark]
    public GenRes<int, int> CreateErrorWithImplicitOperator()
    {
        return GenRes.Error(40);
    }

    [Benchmark]
    public int MatchOk()
    {
        return _okRes.Match(_identity, _identity);
    }

    [Benchmark]
    public int MatchError()
    {
        return _errorRes.Match(_identity, _identity);
    }

    [Benchmark]
    public GenRes<int, int> BindOk()
    {
        return _okRes.Bind(_monadicIdentity);
    }

    [Benchmark]
    public GenRes<int, int> BindError()
    {
        return _errorRes.Bind(_monadicIdentity);
    }

    [Benchmark]
    public GenRes<int, int> MapOk()
    {
        return _okRes.Map(_identity);
    }

    [Benchmark]
    public GenRes<int, int> MapError()
    {
        return _errorRes.Map(_identity);
    }
}