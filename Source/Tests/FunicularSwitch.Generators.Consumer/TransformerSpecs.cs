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
}

[Monad(typeof(Result<>))]
public static class ResultM
{
    public static Result<B> Bind<A, B>(this Result<A> ma, Func<A, Result<B>> fn) => ma.Bind(fn);

    public static Result<A> Return<A>(A a) => Result.Ok(a);
}

[Monad(typeof(Option<>))]
public static class OptionM
{
    public static Option<B> Bind<A, B>(this Option<A> ma, Func<A, Option<B>> fn) => ma.Bind(fn);

    public static Option<A> Return<A>(A a) => Option.Some(a);
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

public record Writer<A>
{
    private Writer(A Value, string Log)
    {
        this.Value = Value;
        this.Log = $"{Log,-21}: {Value}";
    }

    public string Log { get; init; }

    public A Value { get; init; }

    public static Writer<A> Append(A v, string text) => new(v, text);

    public Writer<B> Bind<B>(Func<A, Writer<B>> fn)
    {
        var tmp = fn(Value);
        return tmp with {Log = Log + "\n" + tmp.Log};
    }

    public static Writer<A> Init(A v) => new(v, "init");
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
    public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<IEnumerable<B>, TMB> returnM,
        Func<TMA, Func<IEnumerable<A>, TMB>, TMB> bindM) =>
        throw new NotImplementedException();

    internal static M<IEnumerable<B>> Bind<M, A, B>(M<IEnumerable<A>> ma, Func<A, M<IEnumerable<B>>> fn)
        where M : struct, MOps<M> =>
        default(M).Bind(
            ma,
            xs => xs.Aggregate(
                default(M).Return<IEnumerable<B>>([]),
                (cur, acc) => default(M).Bind<IEnumerable<B>, IEnumerable<B>>(
                    cur,
                    xs_ => default(M).Map<IEnumerable<B>, IEnumerable<B>>(
                        fn(acc),
                        y => [..xs_, ..y]))));
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

public partial class ResultEnumerable
{
    public static ResultEnumerable<B> Bind2<A, B>(this ResultEnumerable<A> ma, Func<A, ResultEnumerable<B>> fn) =>
        ((ResultOps.M_<IEnumerable<B>>) EnumerableT.Bind<ResultOps, A, B>((ResultOps.M_<IEnumerable<A>>) ma.M, a => (ResultOps.M_<IEnumerable<B>>) fn(a).M)).M;

    private struct ResultOps : MOps<ResultOps>
    {
        public M<B> Bind<A, B>(M<A> ma, Func<A, M<B>> fn) => (M_<B>) ((M_<A>) ma).M.Bind(x => ((M_<B>) fn(x)).M);

        public M<A> Return<A>(A value) => (M_<A>) Result.Ok(value);

        public readonly record struct M_<A>(Result<A> M) : M<A>
        {
            public static implicit operator M_<A>(Result<A> ma) => new(ma);

            public static implicit operator Result<A>(M_<A> ma) => ma.M;
        }
    }
}

internal interface MOps<T> where T : struct, MOps<T>
{
    public M<B> Bind<A, B>(M<A> ma, Func<A, M<B>> fn);

    public M<B> Map<A, B>(M<A> ma, Func<A, B> fn) => Bind(ma, x => Return(fn(x)));

    public M<A> Return<A>(A value);
}

internal interface M<A>;
