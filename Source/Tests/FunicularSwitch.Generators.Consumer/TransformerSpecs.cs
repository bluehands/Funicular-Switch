using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class TransformerSpecs
{
    [TestMethod]
    public void ResultOption_Return()
    {
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

[MonadTransformer(typeof(ResultM))]
public static class ResultT
{
    public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<Result<B>, TMB> returnM,
        Func<TMA, Func<Result<A>, TMB>, TMB> bindM) =>
        bindM(ma, aResult => aResult.Match(fn, e => returnM(Result.Error<B>(e))));
}

[TransformMonad(typeof(OptionM), typeof(ResultT))]
public partial record OptionResult<A>;