using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

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
        var result = value.Bind(x => ResultOption.Return(x.ToString()));

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
        var result = value.Bind(x => ResultOption.Return(x.ToString()));

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
        var w2 = WriterResult.InitOk(1337)
            .Bind<int, int>(x => Writer<Result<int>>.Append(x * 2, "multiplied by 2"))
            .Bind<int, int>(x => Writer<Result<int>>.Append(x + 100, "added 100"))
            .Bind<int, string>(x => Writer<Result<string>>.Append(Result.Error<string>("conversion failed"), "to hex failed"));
        Console.WriteLine(w2.M.Log);
    }

    [TestMethod]
    public void TODO_WriterResultOption()
    {
        Console.WriteLine();
        Console.WriteLine("WriterResultOption<>");
        var w2 = WriterResultOption.InitOk(1337)
            .Bind<int, int>(x => Writer<Result<Option<int>>>.Append(Option.Some(x * 2), "multiplied by 2"))
            .Bind<int, int>(x => Writer<Result<Option<int>>>.Append(Option.None<int>(), "added 100 -> none"))
            .Bind<int, string>(x => Writer<Result<Option<string>>>.Append(Result.Error<Option<string>>("conversion failed"), "to hex failed"));
        Console.WriteLine(w2.M.Log);
    }

    [TestMethod]
    public void TODO_WriterResultOption2()
    {
        Console.WriteLine("WriterResultOption2<>");
        var w2 = WriterResultOption2.InitOk(1337)
            .Bind<int, int>(x => WriterResult.Append(Option.Some(x * 2), "multiplied by 2"))
            .Bind<int, int>(x => WriterResult.Append(Option.None<int>(), "added 100 -> none"))
            .Bind<int, string>(x => (WriterResult<Option<string>>) Writer<Result<Option<string>>>.Append(Result.Error<Option<string>>("conversion failed"), "to hex failed"));
        Console.WriteLine(w2.M.M.Log);
    }
}

public static class ResultM
{
    public static Result<B> Bind<A, B>(this Result<A> ma, Func<A, Result<B>> fn) => ma.Bind(fn);

    public static Result<A> Return<A>(A a) => Result.Ok(a);
}

public static class OptionM
{
    public static Option<B> Bind<A, B>(this Option<A> ma, Func<A, Option<B>> fn) => ma.Bind(fn);

    public static Option<A> Return<A>(A a) => Option.Some(a);
}

[MonadTransformer(typeof(OptionM))]
public static class OptionT
{
    public static Monad<Option<B>> Bind<A, B>(Monad<Option<A>> ma, Func<A, Monad<Option<B>>> fn) =>
        ma.Bind(aOption => aOption.Match(fn, () => ma.Return(Option.None<B>())));
}

[TransformMonad(typeof(ResultM), typeof(OptionT))]
public partial record ResultOption<A>;

[MonadTransformer(typeof(Result<>))]
public static class ResultT
{
    public static Monad<Result<B>> Bind<A, B>(Monad<Result<A>> ma, Func<A, Monad<Result<B>>> fn) =>
        ma.Bind(aResult => aResult.Match(fn, e => ma.Return(Result.Error<B>(e))));
}

[TransformMonad(typeof(OptionM), typeof(ResultT))]
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
    public static Monad<IEnumerable<B>> Bind<A, B>(Monad<IEnumerable<A>> ma, Func<A, Monad<IEnumerable<B>>> fn) =>
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

[TransformMonad(typeof(WriterResult<>), typeof(OptionT))]
public readonly partial record struct WriterResultOption2<A>;

// TODO: use like enumerable
[TransformMonad(typeof(Result<>), typeof(EnumerableT))]
public readonly partial record struct ResultEnumerable<A>;

public partial class ResultEnumerable
{
    public static ResultEnumerable<B> Bind2<A, B>(this ResultEnumerable<A> ma, Func<A, ResultEnumerable<B>> fn) =>
        ((ResultMonad<IEnumerable<B>>) EnumerableT.Bind<A, B>((ResultMonad<IEnumerable<A>>) ma.M, a => (ResultMonad<IEnumerable<B>>) fn(a).M)).M;
    
    private readonly record struct ResultMonad<A>(Result<A> M) : Monad<A>
    {
        public Monad<B> Bind<B>(Func<A, Monad<B>> fn) => (ResultMonad<B>) M.Bind<B>(a => (ResultMonad<B>) fn(a));

        public Monad<B> Return<B>(B value) => (ResultMonad<B>) Result.Ok(value);

        public B Cast<B>() => (B)(object)M;

        public static implicit operator ResultMonad<A>(Result<A> ma) => new(ma);

        public static implicit operator Result<A>(ResultMonad<A> ma) => ma.M;
    }
}
