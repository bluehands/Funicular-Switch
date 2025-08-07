using System;

namespace FunicularSwitch;

public class OptionT
{
    public static TMA Return<A, TMA>(A a, Func<Option<A>, TMA> returnM) =>
        returnM(Option.Some(a));

    public static TMB Bind<A, B, TMA, TMB>(TMA ma, Func<A, TMB> fn, Func<Option<B>, TMB> returnM,
        Func<TMA, Func<Option<A>, TMB>, TMB> bindM) =>
        bindM(ma, a => a.Match(fn, () => returnM(Option.None<B>())));

    public static TMA Lift<A, TA, TMA>(TA ma, Func<Option<A>, TMA> returnM,
        Func<TA, Func<A, TMA>, TMA> bindM) =>
        bindM(ma, a => returnM(Option.Some(a)));
}

public static class ResultOption
{
    public static ResultOption<A> Return<A>(A a) => OptionT.Return(a, Result.Ok);

    public static ResultOption<A> Lift<A>(Result<A> ma) =>
        OptionT.Lift<A, Result<A>, Result<Option<A>>>(ma, Result.Ok, ResultBind);
    
    public static ResultOption<B> Bind<A, B>(this ResultOption<A> ma, Func<A, ResultOption<B>> fn) =>
        OptionT.Bind<A, B, Result<Option<A>>, Result<Option<B>>>(ma, x => fn(x), Result.Ok, ResultBind);

    public static ResultOption<B> Map<A, B>(this ResultOption<A> ma, Func<A, B> fn) =>
        ma.Bind(a => Return(fn(a)));

    private static Result<B> ResultBind<A, B>(Result<A> ma, Func<A, Result<B>> fn) => ma.Bind(fn);
}

public readonly record struct ResultOption<A>(Result<Option<A>> M)
{
    public static implicit operator ResultOption<A>(Result<Option<A>> ma) => new(ma);
    
    public static implicit operator Result<Option<A>>(ResultOption<A> ma) => ma.M;
}

public static class Playground
{
    public static void Play()
    {
        var returned = OptionT.Return(123, Result.Ok);
        var bound = OptionT.Bind<int, int, Result<Option<int>>, Result<Option<int>>>(returned,
            a => Result.Ok(Option.Some(a * 200)), Result.Ok, ResultBind);
        var lifted = OptionT.Lift<int, Result<int>, Result<Option<int>>>(Result.Ok(123), Result.Ok, ResultBind);

        var returned2 = ResultOption.Return("henlo");
        var bound2 = returned2.Bind(a => ResultOption.Return(a.Length)).Map(x => x * 200);
        var lifted2 = ResultOption.Lift(Result.Ok("omg"));

        static Result<B> ResultBind<A, B>(Result<A> ma, Func<A, Result<B>> fn) => ma.Bind(fn);
    }
}