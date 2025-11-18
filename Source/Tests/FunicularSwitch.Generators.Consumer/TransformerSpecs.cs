using FluentAssertions;
using FluentAssertions.Execution;
using FunicularSwitch.Transformers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

// ReSharper disable InconsistentNaming

namespace FunicularSwitch.Generators.Consumer;

[TestClass]
public class TransformerSpecs
{
    [TestMethod]
    [DataRow(0, 1, "Ok ")]
    [DataRow(-1, 0, "Ok ")]
    [DataRow(-1, 1, "Error A value is 2")]
    [DataRow(-2, 1, "Ok 4")]
    [DataRow(-2, 2, "Error A value is 2")]
    [DataRow(-10, 5, "Ok 20,18,16,14,12")]
    public void ResultEnumerable_UseCase(int start, int end, string expectedResult)
    {
        // Arrange
        var initial = Enumerable.Range(start, end);

        // Act
        var result = ResultEnumerable.Ok(initial)
            .Select(x => x * -2)
            .Where(x => x > 0)
            .Assert(x => x != 2, () => "A value is 2");

        // Assert
        result.M.Map(x => string.Join(",", x)).ToString().Should().Be(expectedResult);
    }

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
    [DataRow(2, 2, "Right 4", "")]
    [DataRow(0, 2, "Right 2", "value == 0")]
    [DataRow(2, 0, "Right 2", "value == 0")]
    [DataRow(2, -1, "Left value < 0", "value < 0")]
    [DataRow(-1, 2, "Left value < 0", "value < 0")]
    [DataRow(0, -1, "Left value < 0", """
                                      value == 0
                                      value < 0
                                      """)]
    [DataRow(-1, 0, "Left value < 0", "value < 0")]
    public void WriterEither_Combine(int a, int b, string expectedResult, string expectedLog)
    {
        // Act
        var result =
            from t in WriterEither.Combine(From(a), From(b))
            let c = t.Item1 + t.Item2
            select c;

        // Assert
        using (new AssertionScope())
        {
            string.Join(Environment.NewLine, result.Log).Should().Be(expectedLog);
            ((string) result.Value.ToString()).Should().Be(expectedResult);
        }

        Writer2<string, Either<string, int>> From(int value) => value switch
        {
            < 0 => WriterEither.Left<string, int>("value < 0"),
            0 => WriterEither.Append<string, string, int>(value, "value == 0"),
            _ => WriterEither.InitRight<string, string, int>(value),
        };
    }

    [TestMethod]
    [DataRow(1, 1, "Right 0", """
                              1/1 = 1
                              1-1 = 0
                              sqrt(0) = 0
                              """)]
    [DataRow(2, 2, "Left sqrt(-1) -> Cannot get square root of negative number", """
                                                                                 2/2 = 1
                                                                                 1-2 = -1
                                                                                 sqrt(-1) -> Cannot get square root of negative number
                                                                                 """)]
    [DataRow(2, 0, "Left 2/0 -> Cannot divide by 0", "2/0 -> Cannot divide by 0")]
    public void WriterEither_UseCase(int a, int b, string expectedResult, string expectedLog)
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
            string.Join(Environment.NewLine, result.Log).Should().Be(expectedLog);
            ((string) result.Value.ToString()).Should().Be(expectedResult);
        }

        static Writer2<string, Either<string, int>> Sqrt(int a) =>
            a < 0
                ? WriterEither.Left<string, int>($"sqrt({a}) -> Cannot get square root of negative number")
                : WriterEither.Append<string, string, int>((int) Math.Sqrt(a), v => $"sqrt({a}) = {v}");

        static Writer2<string, Either<string, int>> Div(int a, int b) =>
            b == 0
                ? WriterEither.Left<string, int>($"{a}/{b} -> Cannot divide by 0")
                : WriterEither.Append<string, string, int>(a / b, v => $"{a}/{b} = {v}");

        static Writer2<string, Either<string, int>> Subtract(int a, int b) =>
            WriterEither.Append<string, string, int>(a - b, v => $"{a}-{b} = {v}");
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
            string.Join(Environment.NewLine, result.Log).Should().Be(expectedLog);
            ((string) result.Value.ToString()).Should().Be(expectedResult);
        }

        static Writer<FunicularSwitch.Result<int>> Sqrt(int a) =>
            a < 0
                ? WriterResult.Error<int>($"sqrt({a}) -> Cannot get square root of negative number")
                : WriterResult.Append((int) Math.Sqrt(a), v => $"sqrt({a}) = {v}");

        static Writer<FunicularSwitch.Result<int>> Div(int a, int b) =>
            b == 0
                ? WriterResult.Error<int>($"{a}/{b} -> Cannot divide by 0")
                : WriterResult.Append(a / b, v => $"{a}/{b} = {v}");

        static Writer<FunicularSwitch.Result<int>> Subtract(int a, int b) =>
            WriterResult.Append(a - b, v => $"{a}-{b} = {v}");
    }
}

[TransformMonad(typeof(Result<>), typeof(OptionT))]
public partial record ResultOption<A>;

[TransformMonad(typeof(Option<>), typeof(ResultT))]
public partial record OptionResult<A>;

public record Writer<A>(A Value, IReadOnlyList<string> Log);

[ExtendMonad]
public static partial class Writer
{
    public static Writer<A> Append<A>(A v, string log) => new(v, [log]);

    public static Writer<B> Bind<A, B>(this Writer<A> ma, Func<A, Writer<B>> fn)
    {
        var tmp = fn(ma.Value);
        var result = tmp with {Log = [..ma.Log, ..tmp.Log]};
        return result;
    }

    public static Writer<A> Init<A>(A v) => new(v, []);
}

[TransformMonad(typeof(Writer), typeof(ResultT))]
public static partial class WriterResult
{
    public static Writer<FunicularSwitch.Result<A>> Append<A>(A value, string text) => Writer.Append<FunicularSwitch.Result<A>>(value, text);

    public static Writer<FunicularSwitch.Result<A>> Append<A>(A value, Func<A, string> textFn) => Append(value, textFn(value));

    public static Writer<FunicularSwitch.Result<A>> Error<A>(string error) => Writer.Append(FunicularSwitch.Result.Error<A>(error), error);
}

[TransformMonad(typeof(Result<>), typeof(EnumerableT))]
public readonly partial record struct ResultEnumerable<A>;

public static partial class ResultEnumerable
{
    public static ResultEnumerable<A> Assert<A>(this ResultEnumerable<A> ma, Func<A, bool> assertion,
        Func<string> getError) =>
        ma.Bind(a => assertion(a) ? OkYield(a) : Error<A>(getError()));

    public static ResultEnumerable<A> Empty<A>() => Ok<A>([]);

    public static ResultEnumerable<A> Error<A>(string details) => Result.Error<IEnumerable<A>>(details);

    public static ResultEnumerable<A> Ok<A>(IEnumerable<A> xs) => Result.Ok(xs);

    public static ResultEnumerable<A> Where<A>(this ResultEnumerable<A> ma, Func<A, bool> predicate) =>
        ma.Bind(a => predicate(a) ? OkYield(a) : Empty<A>());
}

public record Writer2<L, A>(A Value, IReadOnlyList<L> Log);

[ExtendMonad]
public static partial class Writer2
{
    public static Writer2<L, A> Append<L, A>(A v, L log) => new(v, [log]);

    public static Writer2<L, B> Bind<L, A, B>(this Writer2<L, A> ma, Func<A, Writer2<L, B>> fn)
    {
        var tmp = fn(ma.Value);
        var result = tmp with {Log = [..ma.Log, ..tmp.Log]};
        return result;
    }

    public static Writer2<L, A> Init<L, A>(A v) => new(v, []);
}

public abstract record Either<B, A>
{
    public record Left(B Value) : Either<B, A>
    {
        public override string ToString() => $"Left {Value}";
    }

    public record Right(A Value) : Either<B, A>
    {
        public override string ToString() => $"Right {Value}";
    }
}

[ExtendMonad]
[MonadTransformer(typeof(Either))]
public static partial class Either
{
    public static Either<B, C> Bind<B, A, C>(this Either<B, A> ma, Func<A, Either<B, C>> fn) => ma switch
    {
        Either<B, A>.Left left => Left<B, C>(left.Value),
        Either<B, A>.Right right => fn(right.Value),
        _ => throw new ArgumentOutOfRangeException(),
    };

    public static Monad<Either<B, C>> BindT<B, A, C>(Monad<Either<B, A>> ma, Func<A, Monad<Either<B, C>>> fn) =>
        ma.Bind(eitherA => eitherA switch
        {
            Either<B, A>.Left left => ma.Return(Left<B, C>(left.Value)),
            Either<B, A>.Right right => fn(right.Value),
            _ => throw new ArgumentOutOfRangeException(),
        });

    public static Either<B, A> Left<B, A>(B b) => new Either<B, A>.Left(b);

    public static Either<B, A> Right<B, A>(A a) => new Either<B, A>.Right(a);
}

[TransformMonad(typeof(Writer2), typeof(Either))]
public static partial class WriterEither
{
    public static Writer2<L, Either<B, A>> Append<L, B, A>(A value, L log) =>
        Writer2.Append(Either.Right<B, A>(value), log);

    public static Writer2<L, Either<B, A>> Append<L, B, A>(A value, Func<A, L> logFn) =>
        Append<L, B, A>(value, logFn(value));

    public static Writer2<B, Either<B, A>> Left<B, A>(B b) => Writer2.Append(Either.Left<B, A>(b), b);
}
