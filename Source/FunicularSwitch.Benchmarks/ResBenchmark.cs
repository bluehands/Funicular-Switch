using BenchmarkDotNet.Attributes;

namespace FunicularSwitch.Benchmarks;

[MemoryDiagnoser()]
public class ResBenchmark
{
    private Result<int> _okRes;
    private Result<int> _errorRes;
    private Func<int, int> _identity;
    private Func<string, int> _errorToInt;
    private Func<int, Result<int>> _monadicIdentity;

    [GlobalSetup]
    public void Setup()
    {
        _identity = static i => i;
        _errorToInt = static _ => 42;
        _monadicIdentity = Result<int>.Ok;
        _okRes = Result<int>.Ok(42);
        _errorRes = Result<int>.Error("error");
    }

    [Benchmark]
    public Result<int> CreateOkWithType()
    {
        return Result<int>.Ok(42);
    }

    [Benchmark]
    public Result<int> CreateErrorWithType()
    {
        return Result<int>.Error("error");
    }

    [Benchmark]
    public Result<int> CreateOkWithImplicitOperator()
    {
        return Result.Ok(42);
    }

    [Benchmark]
    public Result<int> CreateErrorWithImplicitOperator()
    {
        return Result.Error<int>("error");
    }

    [Benchmark]
    public int MatchOk()
    {
        return _okRes.Match(_identity, _errorToInt);
    }

    [Benchmark]
    public int MatchError()
    {
        return _errorRes.Match(_identity, _errorToInt);
    }

    [Benchmark]
    public Result<int> BindOk()
    {
        return _okRes.Bind(_monadicIdentity);
    }

    [Benchmark]
    public Result<int> BindError()
    {
        return _errorRes.Bind(_monadicIdentity);
    }

    [Benchmark]
    public Result<int> MapOk()
    {
        return _okRes.Map(_identity);
    }

    [Benchmark]
    public Result<int> MapError()
    {
        return _errorRes.Map(_identity);
    }
}