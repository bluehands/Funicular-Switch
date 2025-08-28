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
            string.Join(Environment.NewLine, result.M.Log).Should().Be(expectedLog);
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
}

[TransformMonad(typeof(Result<>), typeof(OptionT))]
public partial record ResultOption<A>;

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

[TransformMonad(typeof(Writer), typeof(ResultT))]
public readonly partial record struct WriterResult<A>;

public static partial class WriterResult
{
    public static WriterResult<A> Append<A>(A value, string text) => Writer.Append<FunicularSwitch.Result<A>>(value, text);
    
    public static WriterResult<A> Append<A>(A value, Func<A, string> textFn) => Append(value, textFn(value));

    public static WriterResult<A> Error<A>(string error) => Writer.Append(FunicularSwitch.Result.Error<A>(error), error);
}

[TransformMonad(typeof(Result<>), typeof(EnumerableT))]
public readonly partial record struct ResultEnumerable<A>;

public static partial class ResultEnumerable
{
    public static ResultEnumerable<A> Empty<A>() => Ok<A>([]);
    
    public static ResultEnumerable<A> Ok<A>(IEnumerable<A> xs) => Result.Ok(xs);
    
    public static ResultEnumerable<A> Error<A>(string details) => Result.Error<IEnumerable<A>>(details);

    public static ResultEnumerable<A> Where<A>(this ResultEnumerable<A> ma, Func<A, bool> predicate) =>
        ma.Bind(a => predicate(a) ? OkYield(a) : Empty<A>());

    public static ResultEnumerable<A> Assert<A>(this ResultEnumerable<A> ma, Func<A, bool> assertion,
        Func<string> getError) =>
        ma.Bind(a => assertion(a) ? OkYield(a) : Error<A>(getError()));
}
