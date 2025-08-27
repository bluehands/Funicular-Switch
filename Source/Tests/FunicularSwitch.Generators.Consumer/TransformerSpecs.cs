using FluentAssertions;
using FluentAssertions.Execution;
using Microsoft.VisualStudio.TestTools.UnitTesting;
// ReSharper disable InconsistentNaming

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class TransformerSpecs
{
    [TestMethod]
    public void ResultOption_Bind_WithError()
    {
        // Arrange
        var value = ResultOption.Lift(Result.Error<int>("test"));
        var expected = new ResultOption<string>(Result.Error<Option<string>>("test"));

        // Act
        var result = value.Bind(x => ResultOption.OkSome(x.ToString()));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Bind_WithOkNone()
    {
        // Arrange
        var value = (ResultOption<int>) Result.Ok(Option.None<int>());
        var expected = new ResultOption<string>(Result.Ok(Option.None<string>()));

        // Act
        var result = value.Bind(x => ResultOption.OkSome(x.ToString()));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Bind_WithOkSome()
    {
        // Arrange
        var value = ResultOption.OkSome(1337);
        var expected = new ResultOption<string>(Result.Ok(Option.Some("1337")));

        // Act
        var result = value.Bind(x => ResultOption.OkSome(x.ToString()));

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Lift()
    {
        // Arrange
        var value = Result.Ok(1337);
        var expected = ResultOption.OkSome(1337);

        // Act
        var result = ResultOption.Lift(value);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void ResultOption_Return()
    {
        // Arrange
        var value = 1337;
        var expected = new ResultOption<int>(Result.Ok(Option.Some(value)));

        // Act
        var result = ResultOption.OkSome(value);

        // Assert
        result.Should().BeEquivalentTo(expected);
    }

    [TestMethod]
    public void TODO_ResultEnumerable()
    {
        Console.WriteLine("ResultEnumerable<>");
        var r =
            from a in ResultEnumerable.OkYield(24)
            from b in Result<IEnumerable<int>>.Ok(Enumerable.Range(0, a))
            from c in ResultEnumerable.OkYield(b.ToString("X2"))
            from d in Result<IEnumerable<int>>.Error("meh")
            select d;
        Console.WriteLine(r.M.Map(x => string.Join("\n", x)));
    }

    [TestMethod]
    [DataRow(1, 1, "Ok 0", """
                            1/1 = 1
                            1-1 = 0
                            sqrt(0) = 0
                            """)]
    [DataRow(2, 2, "Error sqrt(-1) -> Cannot get square root of negative number", """
                                                                                    2/2 = 1
                                                                                    1-2 = -1
                                                                                    sqrt(-1) -> Cannot get square root of negative number
                                                                                    """)]
    [DataRow(2, 0, "Error 2/0 -> Cannot divide by 0", "2/0 -> Cannot divide by 0")]
    public void WriterResult_UseCase(int a, int b, string expectedResult, string expectedLog)
    {
        // Act
        var result =
            from x in Div(a, b)
            from y in Subtract(x, b)
            from z in Sqrt(y)
            select y;

        // Assert
        using (new AssertionScope())
        {
            string.Join("\n", result.M.Log).Should().Be(expectedLog);
            result.M.Value.ToString().Should().Be(expectedResult);
        }

        static WriterResult<int> Sqrt(int a) =>
            a < 0
                ? WriterResult.Error<int>($"sqrt({a}) -> Cannot get square root of negative number")
                : WriterResult.Append((int)Math.Sqrt(a), v => $"sqrt({a}) = {v}");

        static WriterResult<int> Div(int a, int b) =>
            b == 0
                ? WriterResult.Error<int>($"{a}/{b} -> Cannot divide by 0")
                : WriterResult.Append(a / b, v => $"{a}/{b} = {v}");

        static WriterResult<int> Subtract(int a, int b) =>
            WriterResult.Append(a - b, v => $"{a}-{b} = {v}");
    }
}

[MonadTransformer(typeof(Option<>))]
public static class OptionT
{
    public static Monad<Option<B>> BindT<A, B>(Monad<Option<A>> ma, Func<A, Monad<Option<B>>> fn) =>
        ma.Bind(aOption => aOption.Match(fn, () => ma.Return(Option.None<B>())));
}

[TransformMonad(typeof(Result<>), typeof(OptionT))]
public partial record ResultOption<A>;

[MonadTransformer(typeof(Result<>))]
public static class ResultT
{
    public static Monad<Result<B>> BindT<A, B>(Monad<Result<A>> ma, Func<A, Monad<Result<B>>> fn) =>
        ma.Bind(aResult => aResult.Match(fn, e => ma.Return(Result.Error<B>(e))));
}

[TransformMonad(typeof(Option<>), typeof(ResultT))]
public partial record OptionResult<A>;

public record Writer<A>(A Value, IReadOnlyList<string> Log);

public static class Writer
{
    public static Writer<A> Init<A>(A v) => new(v, []);
    
    public static Writer<A> Append<A>(A v, string log) => new(v, [log]);
    
    public static Writer<B> Bind<A, B>(this Writer<A> ma, Func<A, Writer<B>> fn)
    {
        var tmp = fn(ma.Value);
        var result = tmp with {Log = [..ma.Log, ..tmp.Log]};
        return result;
    }
}

public static class EnumerableM
{
    public static IEnumerable<B> Bind<A, B>(this IEnumerable<A> ma, Func<A, IEnumerable<B>> fn) =>
        ma.SelectMany(fn);

    public static IEnumerable<A> Yield<A>(A a) => [a];
}

[MonadTransformer(typeof(EnumerableM))]
public static class EnumerableT
{
    public static Monad<IEnumerable<B>> BindT<A, B>(Monad<IEnumerable<A>> ma, Func<A, Monad<IEnumerable<B>>> fn) =>
        ma.Bind(xs => xs.Aggregate(
            ma.Return<IEnumerable<B>>([]),
            (cur, acc) => cur.Bind(xs_ =>
                fn(acc).Map(y => (IEnumerable<B>) [..xs_, ..y]))));
}

[TransformMonad(typeof(Writer), typeof(ResultT))]
public readonly partial record struct WriterResult<A>;

public static partial class WriterResult
{
    public static WriterResult<A> Append<A>(A value, string text) => Writer.Append<Result<A>>(value, text);
    
    public static WriterResult<A> Append<A>(A value, Func<A, string> textFn) => Append(value, textFn(value));

    public static WriterResult<A> Error<A>(string error) => Writer.Append(Result.Error<A>(error), error);
}

// TODO: use like enumerable
[TransformMonad(typeof(Result<>), typeof(EnumerableT))]
public readonly partial record struct ResultEnumerable<A>;
