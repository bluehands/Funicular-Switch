using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class TransformerSpecs
{
    [TestMethod]
    public void ResultOption_Return()
    {
        var x = ResultOption.Return(1337);
        var y = x.Bind(x_ => ResultOption.Return("hi"));
        
        // Arrange
        var value = 1337;
        var expected = new ResultOption<int>(Result.Ok(Option.Some(value)));

        // Act
        var result = ResultOption.Return(value);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Bind_WithOkSome()
    {
        // Arrange
        var value = ResultOption.Return(1337);
        var expected = new ResultOption<string>(Result.Ok(Option.Some("1337")));

        // Act
        var result = value.Bind(x => ResultOption.Return(x.ToString()));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Lift()
    {
        // Arrange
        var value = Result.Ok(1337);
        var expected = ResultOption.Return(1337);

        // Act
        var result = ResultOption.Lift(value);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Bind_WithOkNone()
    {
        // Arrange
        var value = (ResultOption<int>)Result.Ok(Option.None<int>());
        var expected = new ResultOption<string>(Result.Ok(Option.None<string>()));

        // Act
        var result = value.Bind(x => ResultOption.Return(x.ToString()));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Bind_WithError()
    {
        // Arrange
        var value = ResultOption.Lift(Result.Error<int>("test"));
        var expected = new ResultOption<string>(Result.Error<Option<string>>("test"));

        // Act
        var result = value.Bind(x => ResultOption.Return(x.ToString()));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }
}

[Monad(typeof(Result<>))]
public static class ResultM
{
    public static Result<A> Return<A>(A a) => Result.Ok(a);

    public static Result<B> Bind<A, B>(this Result<A> ma, Func<A, Result<B>> fn) => ma.Bind(fn);
}

[Monad(typeof(Option<>))]
public static class OptionM
{
    public static Option<A> Return<A>(A a) => Option.Some(a);

    public static Option<B> Bind<A, B>(this Option<A> ma, Func<A, Option<B>> fn) => ma.Bind(fn);
}

[MonadTransformer(typeof(OptionM))]
public static class OptionT
{
    public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<Option<B>, TMB> returnM,
        Func<TMA, Func<Option<A>, TMB>, TMB> bindM) =>
        bindM(ma, aOption => aOption.Match(fn, () => returnM(Option.None<B>())));
}

[TransformMonad(typeof(ResultM), typeof(OptionT))]
public partial record ResultOption<A>;

[MonadTransformer(typeof(Result<>))]
public static class ResultT
{
    public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<Result<B>, TMB> returnM,
        Func<TMA, Func<Result<A>, TMB>, TMB> bindM) =>
        bindM(ma, aResult => aResult.Match(fn, e => returnM(Result.Error<B>(e))));
}

[TransformMonad(typeof(OptionM), typeof(ResultT))]
public partial record OptionResult<A>;

public record Writer<A>(A Value, string Log)
{
    public static Writer<A> Init(A v) => new(v, string.Empty);

    public Writer<B> Bind<B>(Func<A, Writer<B>> fn)
    {
        var tmp = fn(Value);
        return new(tmp.Value, Log + tmp.Log);
    }
}

public static class EnumerableM
{
    public static IEnumerable<A> Yield<A>(A v) => throw new NotImplementedException();

    public static IEnumerable<B> Bind<A, B>(this IEnumerable<A> en, Func<A, IEnumerable<B>> fn) =>
        throw new NotImplementedException();
}

[MonadTransformer(typeof(EnumerableM))]
public static class EnumerableT
{
    public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<IEnumerable<B>, TMB> returnM,
        Func<TMA, Func<IEnumerable<A>, TMB>, TMB> bindM) =>
        throw new NotImplementedException();
}

// TODO: add use case tests
[TransformMonad(typeof(Writer<>), typeof(ResultT))]
public readonly partial record struct WriterResult<A>;

// TODO: use like enumerable
[TransformMonad(typeof(Result<>), typeof(EnumerableT))]
public readonly partial record struct ResultEnumerable<A>
{
    public ResultEnumerable<B> Bind_<B>(Func<A, ResultEnumerable<B>> fn)
    {
        return M.Bind(x => BindInternal(x, fn));
        
        static Result<IEnumerable<B>> BindInternal(IEnumerable<A> xs, Func<A, ResultEnumerable<B>> fn) =>
            xs.Aggregate(Result.Ok<IEnumerable<B>>([]),
                (cur, acc) => cur.Bind(xs_ => fn(acc).M.Map<IEnumerable<B>>(y => [..xs_, ..y])));
    }
}