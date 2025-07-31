using BenchmarkDotNet.Attributes;
using FunicularSwitch.Generic;

namespace FunicularSwitch.Benchmarks;

[MemoryDiagnoser()]
public class GenResBenchmark
{
    private GenericResult<int, int> _okRes;
    private GenericResult<int, int> _errorRes;
    private Func<int, int> _identity;
    private Func<int, GenericResult<int, int>> _monadicIdentity;

    [GlobalSetup]
    public void Setup()
    {
        _identity = static i => i;
        _monadicIdentity = GenericResult<int, int>.Ok;
        _okRes = GenericResult<int, int>.Ok(42);
        _errorRes = GenericResult<int, int>.Error(40);
    }

    [Benchmark]
    public GenericResult<int, int> CreateOkWithType()
    {
        return GenericResult<int, int>.Ok(42);
    }

    [Benchmark]
    public GenericResult<int, int> CreateErrorWithType()
    {
        return GenericResult<int, int>.Error(40);
        
        
        
    }
    
    [Benchmark]
    public GenericResult<int, int> CreateOkWithImplicitOperator()
    {
        return GenericResult.Ok(42);
    }
    
    [Benchmark]
    public GenericResult<int, int> CreateErrorWithImplicitOperator()
    {
        return GenericResult.Error(40);
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
    public GenericResult<int, int> BindOk()
    {
        return _okRes.Bind(_monadicIdentity);
    }

    [Benchmark]
    public GenericResult<int, int> BindError()
    {
        return _errorRes.Bind(_monadicIdentity);
    }

    [Benchmark]
    public GenericResult<int, int> MapOk()
    {
        return _okRes.Map(_identity);
    }

    [Benchmark]
    public GenericResult<int, int> MapError()
    {
        return _errorRes.Map(_identity);
    }
}