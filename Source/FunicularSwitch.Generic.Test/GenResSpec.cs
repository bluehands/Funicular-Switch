using FluentAssertions;

namespace FunicularSwitch.Generic.Test;

public class GenResSpec
{
    [Fact]
    public void Map()
    {
        GenRes.Ok(42).WithError<string>().Map(r => r * 2).GetValueOrThrow().Should().Be(84);
        var error = Result.Error<int>("oh no");
        error.Map(r => r * 2).Should().BeEquivalentTo(error);
    }

    [Fact]
    public void Bind()
    {
        var ok = GenRes.Ok(42);
        var error = GenRes.Error<string>("operation failed");

        static GenRes<int, string> Ok(int input) => input;
        GenRes<int, string> Error(int input) => error;

        var okRes = ok.Bind(Ok);
        okRes.GetValueOrDefault(0).Should().Be(42);
        var errRes = ok.Bind(Error);
        errRes.GetErrorOrDefault(string.Empty).Should().Be("operation failed");

        GenRes<int, int> x = ok;
        var _ = x.Bind<int>(_ => GenRes.Error(42));
    }

    [Fact]
    public void NotInitializedTrowsOnFirstUse()
    {
        GenOk<string> ok = default;
        var okFn = () => ok.WithError<string>();
        okFn.Should().Throw<InvalidOperationException>();

        GenError<string> err = default;
        var errFn = () => err.WithOk<int>();
        errFn.Should().Throw<InvalidOperationException>();

        GenRes<int, string> res = default;
        var resFn = () => res.IsOk();
        resFn.Should().Throw<InvalidOperationException>();
    }

    [Fact]
    public void AssertCompilerUsesLinqQueryExpressions()
    {
        GenRes<Unit, string> okRes0 =
            from x in GenRes.Ok(42)
            from y in GenRes.Ok<int, string>(1)
            select No.Thing;

        Task<GenRes<Unit, string>> okRes1 =
            from x in GenRes.Ok(42)
            from y in GenRes.Ok<int, string>(1).ToTask()
            select No.Thing;

        Task<GenRes<Unit, string>> okRes2 =
            from x in GenRes.Ok(42)
            from y in GenRes.Ok<int, string>(1)
            select Task.FromResult(No.Thing);

        Task<GenRes<Unit, string>> okRes3 =
            from x in GenRes.Ok(42)
            from y in GenRes.Ok<int, string>(1).ToTask()
            select Task.FromResult(No.Thing);

        GenRes<Unit, string> res0 =
            from x in GenRes.Ok<int, string>(42)
            from y in GenRes.Ok<int, string>(1)
            select No.Thing;

        Task<GenRes<Unit, string>> res1 =
            from x in GenRes.Ok<int, string>(42)
            from y in GenRes.Ok<int, string>(1).ToTask()
            select No.Thing;

        Task<GenRes<Unit, string>> res2 =
            from x in GenRes.Ok<int, string>(42)
            from y in GenRes.Ok<int, string>(1)
            select Task.FromResult(No.Thing);

        Task<GenRes<Unit, string>> res3 =
            from x in GenRes.Ok<int, string>(42)
            from y in GenRes.Ok<int, string>(1).ToTask()
            select Task.FromResult(No.Thing);
    }

    [Fact]
    public void TestIsError()
    {
        var ok = GenRes.Ok<int, string>(42);
        var error = GenRes.Error<int, string>("fail");
        GenRes<int, string> uninitialized = default;

        ok.IsError().Should().BeFalse();
        error.IsError().Should().BeTrue();
        var act = () => { _ = uninitialized.IsError(); };
        act.Should().Throw<InvalidOperationException>();
    }
    
    [Fact]
    public void TestIsOk()
    {
        var ok = GenRes.Ok<int, string>(42);
        var error = GenRes.Error<int, string>("fail");
        GenRes<int, string> uninitialized = default;

        ok.IsOk().Should().BeTrue();
        error.IsOk().Should().BeFalse();
        var act = () => { _ = uninitialized.IsOk(); };
        act.Should().Throw<InvalidOperationException>();
    }
}