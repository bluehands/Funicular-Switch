using FluentAssertions;
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
    public void TODO_WriterResult()
    {
        Console.WriteLine("Writer<>");
        var w = Writer<int>.Init(1337)
            .Bind(x => Writer<int>.Append(x * 2, "multiplied by 2"))
            .Bind(x => Writer<int>.Append(x + 100, "added 100"))
            .Bind(x => Writer<string>.Append(Convert.ToHexString(BitConverter.GetBytes(x)), "to hex"));
        Console.WriteLine(w.Log);

        Console.WriteLine();
        Console.WriteLine("WriterResult<>");
        var w2 =
            from a in WriterResult.InitOk(1337)
            from b in Writer<Result<int>>.Append(a * 2, "multiplied by 2")
            from c in Writer<Result<int>>.Append(b + 100, "added 100")
            from d in Writer<Result<string>>.Append(Result.Error<string>("conversion failed"), "to hex failed")
            select d;
        Console.WriteLine(w2.M.Log);
    }

    [TestMethod]
    public void TODO_WriterResultOption()
    {
        Console.WriteLine();
        Console.WriteLine("WriterResultOption<>");
        var w =
            from a in WriterResultOption.InitOkSome(1337)
            from b in WriterResultOption.Append(a * 2, "multiplied by 2")
            from c in (WriterResultOption<int>) Writer<Result<Option<int>>>.Append(Option.None<int>(), "added 100 -> none")
            from d in (WriterResultOption<string>) Writer<Result<Option<string>>>.Append(Result.Error<Option<string>>("conversion failed"), "to hex failed")
            select d;
        Console.WriteLine(w.M.Log);
    }

    [TestMethod]
    public void TODO_WriterResultOption2()
    {
        Console.WriteLine("WriterResultOption2<>");
        var w =
            from a in WriterResultOption2.InitOkSome(1337)
            from b in WriterResultOption2.Append(a * 2, "multiplied by 2")
            from c in WriterResult.Append(Option.None<int>(), "added 100 -> none")
            from d in (WriterResult<Option<string>>) Writer<Result<Option<string>>>.Append(Result.Error<Option<string>>("conversion failed"),
                "to hex failed")
            select d;
        Console.WriteLine(w.M.M.Log);
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

public record Writer<A>
{
    private Writer(A Value, Option<string> Log)
    {
        this.Value = Value;
        this.Log = Log.Map(log => $"{log,-21}: {Value}");
    }

    public Option<string> Log { get; init; }

    public A Value { get; init; }

    public static Writer<A> Append(A v, string text) => new(v, text);

    public Writer<B> Bind<B>(Func<A, Writer<B>> fn)
    {
        var tmp = fn(Value);
        var result = tmp with {Log = string.Join("\n", [..Log, ..tmp.Log])};
        return result;
    }

    public static Writer<A> Init(A v) => new(v, Option<string>.None);
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

[TransformMonad(typeof(Writer<>), typeof(ResultT))]
public readonly partial record struct WriterResult<A>;

public static partial class WriterResult
{
    public static WriterResult<A> Append<A>(A value, string text) => Writer<Result<A>>.Append(value, text);
}

[TransformMonad(typeof(Writer<>), typeof(ResultT), typeof(OptionT))]
public readonly partial record struct WriterResultOption<A>;

public static partial class WriterResultOption
{
    public static WriterResultOption<A> Append<A>(A a, string text) => Writer<Result<Option<A>>>.Append(Result.Ok(Option.Some(a)), text);
}

[TransformMonad(typeof(WriterResult<>), typeof(OptionT))]
public readonly partial record struct WriterResultOption2<A>;

public static partial class WriterResultOption2
{
    public static WriterResultOption2<A> Append<A>(A a, string text) => WriterResult.Append(Option.Some(a), text);
}

// TODO: use like enumerable
[TransformMonad(typeof(Result<>), typeof(EnumerableT))]
public readonly partial record struct ResultEnumerable<A>;
