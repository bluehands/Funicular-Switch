using AwesomeAssertions;
using FunicularSwitch.Generic.Assertions;

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

    [Fact]
    public void CreatingOkResult_ShouldReturnOkResult()
    {
        GenRes<int, string> result = GenRes.Ok(42);
        result.Should().BeOk().Which.Should().Be(42);
    }

    [Fact]
    public void CreatingErrorResult_ShouldReturnErrorResult()
    {
        GenRes<int, string> result = GenRes.Error("error");
        result.Should().BeError().Which.Should().Be("error");
    }

    [Fact]
    public void GivenOkResult_WhenGettingValue_ShouldReturnValue()
    {
        var result = GenRes.Ok<int, string>(42);
        result.Should().BeOk().Which.Should().Be(42);
    }

    [Fact]
    public void GivenErrorResult_WhenGettingError_ShouldReturnError()
    {
        var result = GenRes.Error<int, string>("error");
        result.GetErrorOrThrow().Should().Be("error");
    }

    [Fact]
    public async Task GivenOkResult_WhenMappingAsync_ShouldReturnMappedResult()
    {
        var result = GenRes.Ok<int, string>(42);
        var mapped = await result.Map(x => Task.FromResult(x * 2));
        mapped.Should().BeOk().Which.Should().Be(84);
    }

    [Fact]
    public async Task GivenOkResult_WhenBindingAsync_ShouldReturnBoundResult()
    {
        var result = GenRes.Ok<int, string>(42);
        var bound = await result.Bind(x => Task.FromResult(GenRes.Ok<int, string>(x * 2)));
        bound.Should().BeOk().Which.Should().Be(84);
    }

    [Fact]
    public void GivenOkResult_WhenUsingLinqSyntax_ShouldWork()
    {
        var result =
            from x in GenRes.Ok<int, string>(42)
            from y in GenRes.Ok<int, string>(10)
            select x + y;

        result.Should().BeOk().Which.Should().Be(52);
    }

    [Fact]
    public void GivenErrorResult_WhenUsingLinqSyntax_ShouldPreserveError()
    {
        var result =
            from x in GenRes.Ok<int, string>(42)
            from y in GenRes.Error<int, string>("error")
            select x + y;

        result.Should().BeError().Which.Should().Be("error");
    }

    [Fact]
    public async Task GivenAsyncResult_WhenUsingLinqSyntax_ShouldWork()
    {
        var result = await (
            from x in Task.FromResult(GenRes.Ok<int, string>(42))
            from y in Task.FromResult(GenRes.Ok<int, string>(10))
            select x + y);

        result.Should().BeOk().Which.Should().Be(52);
    }

    [Fact]
    public void GivenResult_WhenMatchingSync_ShouldMatchCorrectly()
    {
        var okResult = GenRes.Ok<int, string>(42);
        var errorResult = GenRes.Error<int, string>("error");

        var okMatched = okResult.Match(
            ok: x => x * 2,
            error: _ => 0);

        var errorMatched = errorResult.Match(
            ok: x => x * 2,
            error: _ => 0);

        okMatched.Should().Be(84);
        errorMatched.Should().Be(0);
    }

    [Fact]
    public async Task GivenResult_WhenMatchingAsync_ShouldMatchCorrectly()
    {
        var okResult = GenRes.Ok<int, string>(42);

        var matched = await okResult.Match(
            ok: x => Task.FromResult(x * 2),
            error: _ => Task.FromResult(0));

        matched.Should().Be(84);
    }

    [Fact]
    public void GivenOkResult_WhenConvertingToOption_ShouldReturnSome()
    {
        var result = GenRes.Ok<int, string>(42);
        var option = result.ToOption();
        option.IsSome().Should().BeTrue();
        option.GetValueOrThrow().Should().Be(42);
    }

    [Fact]
    public void GivenErrorResult_WhenConvertingToOption_ShouldReturnNone()
    {
        var result = GenRes.Error<int, string>("error");
        var option = result.ToOption();
        option.IsNone().Should().BeTrue();
    }

    [Fact]
    public void GivenGenOk_WhenImplicitlyConvertedToGenRes_ShouldBeOk()
    {
        GenOk<int> ok = GenRes.Ok(42);
        GenRes<int, string> result = ok;
        result.Should().BeOk().Which.Should().Be(42);
    }

    [Fact]
    public async Task GivenGenOk_WhenBindingAsync_ShouldReturnBoundResult()
    {
        GenOk<int> ok = GenRes.Ok(42);
        var bound = await ok.BindAsync<int, string>(x =>
            Task.FromResult(GenRes.Ok<int, string>(x * 2)));
        bound.Should().BeOk().Which.Should().Be(84);
    }

    [Fact]
    public void GivenGenError_WhenImplicitlyConvertedToGenRes_ShouldBeError()
    {
        GenError<string> error = GenRes.Error("error");
        GenRes<int, string> result = error;
        result.Should().BeError().Which.Should().Be("error");
    }

    [Fact]
    public void GivenGenError_WhenConvertingWithOk_ShouldReturnErrorResult()
    {
        GenError<string> error = GenRes.Error("error");
        var result = error.WithOk<int>();
        result.Should().BeError().Which.Should().Be("error");
    }

    [Fact]
    public void ToOption_ShouldConvertToOption()
    {
        var result = GenRes.Ok<int, string>(42);
        var option = result.ToOption();
        option.IsSome().Should().BeTrue();
        option.GetValueOrThrow().Should().Be(42);
    }

    [Fact]
    public async Task DoAsync_ShouldExecuteActionOnOkValue()
    {
        var result = GenRes.Ok<int, string>(42);
        var executed = false;
        _ = await result.DoAsync(_ =>
        {
            executed = true;
            return Task.CompletedTask;
        });
        executed.Should().BeTrue();
    }

    [Fact]
    public async Task DoOnErrorAsync_ShouldExecuteActionOnErrorValue()
    {
        var result = GenRes.Error<int, string>("error");
        var executed = false;
        _ = await result.DoOnErrorAsync(_ =>
        {
            executed = true;
            return Task.CompletedTask;
        });
        executed.Should().BeTrue();
    }

    [Fact]
    public async Task GetValueOrDefaultAsync_WithFuncTask_ShouldReturnDefaultForError()
    {
        var result = GenRes.Error<int, string>("error");
        var value = await result.GetValueOrDefaultAsync(() => Task.FromResult(42));
        value.Should().Be(42);
    }

    [Fact]
    public async Task GetValueOrDefaultAsync_WithFuncValueTask_ShouldReturnDefaultForError()
    {
        var result = GenRes.Error<int, string>("error");
        var value = await result.GetValueOrDefaultAsync(() => new ValueTask<int>(42));
        value.Should().Be(42);
    }

    [Fact]
    public void GetErrorOrDefault_WithFunc_ShouldReturnDefaultForOk()
    {
        var result = GenRes.Ok<int, string>(42);
        var error = result.GetErrorOrDefault(() => "default");
        error.Should().Be("default");
    }

    [Fact]
    public async Task GetErrorOrDefaultAsync_WithTask_ShouldReturnDefaultForOk()
    {
        var result = GenRes.Ok<int, string>(42);
        var error = await result.GetErrorOrDefaultAsync(Task.FromResult("default"));
        error.Should().Be("default");
    }

    [Fact]
    public async Task GetErrorOrDefaultAsync_WithValueTask_ShouldReturnDefaultForOk()
    {
        var result = GenRes.Ok<int, string>(42);
        var error = await result.GetErrorOrDefaultAsync(new ValueTask<string>("default"));
        error.Should().Be("default");
    }

    [Fact]
    public async Task GetErrorOrDefaultAsync_WithFuncTask_ShouldReturnDefaultForOk()
    {
        var result = GenRes.Ok<int, string>(42);
        var error = await result.GetErrorOrDefaultAsync(() => Task.FromResult("default"));
        error.Should().Be("default");
    }

    [Fact]
    public async Task GetErrorOrDefaultAsync_WithFuncValueTask_ShouldReturnDefaultForOk()
    {
        var result = GenRes.Ok<int, string>(42);
        var error = await result.GetErrorOrDefaultAsync(() => new ValueTask<string>("default"));
        error.Should().Be("default");
    }

    [Fact]
    public void ToErrorOption_ShouldReturnSomeForError()
    {
        var result = GenRes.Error<int, string>("error");
        var option = result.ToErrorOption();
        option.IsSome().Should().BeTrue();
        option.GetValueOrThrow().Should().Be("error");
    }

    [Fact]
    public void ToOptions_ShouldReturnCorrectTuple()
    {
        var result = GenRes.Ok<int, string>(42);
        var (okOption, errorOption) = result.ToOptions();
        okOption.IsSome().Should().BeTrue();
        errorOption.IsNone().Should().BeTrue();
    }

    [Fact]
    public async Task ToValueTask_ShouldReturnSameResult()
    {
        var result = GenRes.Ok<int, string>(42);
        var valueTask = result.ToValueTask();
        var unwrapped = await valueTask;
        unwrapped.Should().BeOk().Which.Should().Be(42);
    }

    [Fact]
    public void Do_ShouldExecuteActionForOk()
    {
        var executed = false;
        var result = GenRes.Ok<int, string>(42);
        _ = result.Do(_ => executed = true);
        executed.Should().BeTrue();
    }

    [Fact]
    public void Do_ShouldNotExecuteActionForError()
    {
        var executed = false;
        var result = GenRes.Error<int, string>("error");
        _ = result.Do(_ => executed = true);
        executed.Should().BeFalse();
    }

    [Fact]
    public void DoOnError_ShouldExecuteActionForError()
    {
        var executed = false;
        var result = GenRes.Error<int, string>("error");
        _ = result.DoOnError(_ => executed = true);
        executed.Should().BeTrue();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Match_WithMixedAsyncFuncs_ShouldWorkCorrectly(bool isOk)
    {
        GenRes<int, string> result = isOk ? GenRes.Ok<int, string>(42) : GenRes.Error<int, string>("error");

        var matchedTask = result.Match(
            ok: x => Task.FromResult(x * 2),
            error: _ => Task.FromResult(0));
        var matchedValueTask = result.Match(
            ok: x => new ValueTask<int>(x * 2),
            error: _ => new ValueTask<int>(0));

        var taskResult = await matchedTask;
        var valueTaskResult = await matchedValueTask;

        if (isOk)
        {
            taskResult.Should().Be(84);
            valueTaskResult.Should().Be(84);
        }
        else
        {
            taskResult.Should().Be(0);
            valueTaskResult.Should().Be(0);
        }
    }

    [Fact]
    public async Task Bind_WithValueTask_ShouldWorkCorrectly()
    {
        var result = GenRes.Ok<int, string>(42);
        var bound = await result.Bind(x =>
            new ValueTask<GenRes<string, string>>(GenRes.Ok<string, string>(x.ToString())));
        bound.Should().BeOk().Which.Should().Be("42");
    }

    [Fact]
    public async Task Map_WithValueTask_ShouldWorkCorrectly()
    {
        var result = GenRes.Ok<int, string>(42);
        var mapped = await result.Map(x => new ValueTask<string>(x.ToString()));
        mapped.Should().BeOk().Which.Should().Be("42");
    }

    [Fact]
    public async Task MapError_WithAsyncFuncs_ShouldWorkCorrectly()
    {
        var result = GenRes.Error<int, string>("error");
        var mappedTask = await result.MapError(e => Task.FromResult(e + "!"));
        var mappedValueTask = await result.MapError(e => new ValueTask<string>(e + "!"));

        mappedTask.Should().BeError().Which.Should().Be("error!");
        mappedValueTask.Should().BeError().Which.Should().Be("error!");
    }

    [Fact]
    public void Select_AndSelectMany_ShouldWorkCorrectly()
    {
        var result = GenRes.Ok<int, string>(42);

        var selected = result.Select(x => x.ToString());
        selected.Should().BeOk().Which.Should().Be("42");

        var selectManied = result.SelectMany(x => GenRes.Ok<string, string>(x.ToString()));
        selectManied.Should().BeOk().Which.Should().Be("42");
    }

    [Fact]
    public async Task SelectMany_WithAsyncFuncs_ShouldWorkCorrectly()
    {
        var result = GenRes.Ok<int, string>(42);

        var taskResult = await result.SelectMany(x => Task.FromResult(GenRes.Ok<string, string>(x.ToString())));
        taskResult.Should().BeOk().Which.Should().Be("42");

        var valueTaskResult = await result.SelectMany(x =>
            new ValueTask<GenRes<string, string>>(GenRes.Ok<string, string>(x.ToString())));
        valueTaskResult.Should().BeOk().Which.Should().Be("42");
    }

    [Fact]
    public async Task SelectMany_WithValueTaskProjections_ShouldWorkCorrectly()
    {
        var result = GenRes.Ok<int, string>(42);

        var r1 = await result.SelectMany(
            x => new ValueTask<GenRes<string, string>>(GenRes.Ok<string, string>(x.ToString())),
            (_, s) => s + "!");
        r1.Should().BeOk().Which.Should().Be("42!");

        var r2 = await result.SelectMany(
            x => GenRes.Ok<string, string>(x.ToString()),
            (_, s) => new ValueTask<string>(s + "!"));
        r2.Should().BeOk().Which.Should().Be("42!");

        var r3 = await result.SelectMany(
            x => new ValueTask<GenRes<string, string>>(GenRes.Ok<string, string>(x.ToString())),
            (_, s) => new ValueTask<string>(s + "!"));
        r3.Should().BeOk().Which.Should().Be("42!");
    }

    [Fact]
    public void GetValueOrDefault_WithDefaultValue_ShouldReturnValueForOk()
    {
        var result = GenRes.Ok<int, string>(42);
        var value = result.GetValueOrDefault(0);
        value.Should().Be(42);
    }

    [Fact]
    public void GetValueOrDefault_WithFunc_ShouldReturnDefaultForError()
    {
        var result = GenRes.Error<int, string>("error");
        var value = result.GetValueOrDefault(() => 42);
        value.Should().Be(42);
    }

    [Fact]
    public async Task GetValueOrDefaultAsync_WithTask_ShouldReturnDefaultForError()
    {
        var result = GenRes.Error<int, string>("error");
        var value = await result.GetValueOrDefaultAsync(Task.FromResult(42));
        value.Should().Be(42);
    }

    [Fact]
    public async Task GetValueOrDefaultAsync_WithValueTask_ShouldReturnDefaultForError()
    {
        var result = GenRes.Error<int, string>("error");
        var value = await result.GetValueOrDefaultAsync(new ValueTask<int>(42));
        value.Should().Be(42);
    }

    [Fact]
    public void GetErrorOrDefault_WithDefaultValue_ShouldReturnErrorForError()
    {
        var result = GenRes.Error<int, string>("error");
        var error = result.GetErrorOrDefault("default");
        error.Should().Be("error");
    }

    [Fact]
    public async Task ToTask_ShouldReturnSameResult()
    {
        var result = GenRes.Ok<int, string>(42);
        var task = result.ToTask();
        var unwrapped = await task;
        unwrapped.Should().BeOk().Which.Should().Be(42);
    }

    [Fact]
    public async Task DoAsync_WithValueTask_ShouldExecuteActionOnOkValue()
    {
        var result = GenRes.Ok<int, string>(42);
        var executed = false;
        _ = await result.DoAsync(_ =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
    }

    [Fact]
    public async Task DoOnErrorAsync_WithValueTask_ShouldExecuteActionOnErrorValue()
    {
        var result = GenRes.Error<int, string>("error");
        var executed = false;
        _ = await result.DoOnErrorAsync(_ =>
        {
            executed = true;
            return new ValueTask();
        });
        executed.Should().BeTrue();
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task Match_WithMixedTaskPatterns_ShouldWorkCorrectly(bool isOk)
    {
        GenRes<int, string> result = isOk ? GenRes.Ok<int, string>(42) : GenRes.Error<int, string>("error");

        var r1 = await result.Match(
            ok: x => Task.FromResult(x * 2),
            error: _ => 0);

        var r2 = await result.Match(
            ok: x => x * 2,
            error: _ => Task.FromResult(0));

        var r3 = await result.Match(
            ok: x => new ValueTask<int>(x * 2),
            error: _ => 0);

        var r4 = await result.Match(
            ok: x => x * 2,
            error: _ => new ValueTask<int>(0));

        if (isOk)
        {
            r1.Should().Be(84);
            r2.Should().Be(84);
            r3.Should().Be(84);
            r4.Should().Be(84);
        }
        else
        {
            r1.Should().Be(0);
            r2.Should().Be(0);
            r3.Should().Be(0);
            r4.Should().Be(0);
        }
    }

    [Theory]
    [InlineData(true)]
    [InlineData(false)]
    public async Task SelectMany_WithAsyncResultSelectors_ShouldWorkCorrectly(bool isOk)
    {
        var result = isOk ? GenRes.Ok<int, string>(42) : GenRes.Error<int, string>("error");

        var r1 = await result.SelectMany(
            x => GenRes.Ok<string, string>(x.ToString()),
            (_, s) => Task.FromResult(s + "!"));

        var r2 = await result.SelectMany(
            x => Task.FromResult(GenRes.Ok<string, string>(x.ToString())),
            (_, s) => Task.FromResult(s + "!"));

        if (isOk)
        {
            r1.Should().BeOk().Which.Should().Be("42!");
            r2.Should().BeOk().Which.Should().Be("42!");
        }
        else
        {
            r1.Should().BeError();
            r2.Should().BeError();
        }
    }
}