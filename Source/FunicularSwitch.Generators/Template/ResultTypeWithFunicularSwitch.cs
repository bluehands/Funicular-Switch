namespace FunicularSwitch.Generators.Template;

public abstract partial class MyResult
{
    //depends on FunicularSwitch

    //public static MyResult<Unit> Try(Action action, Func<Exception, MyError> formatError)
    //{
    //    try
    //    {
    //        action();
    //        return No.Thing;
    //    }
    //    catch (Exception e)
    //    {
    //        return Error<Unit>(formatError(e));
    //    }
    //}

    //public static async Task<MyResult<Unit>> Try(Func<Task> action, Func<Exception, MyError> formatError)
    //{
    //    try
    //    {
    //        await action();
    //        return No.Thing;
    //    }
    //    catch (Exception e)
    //    {
    //        return Error<Unit>(formatError(e));
    //    }
    //}
}

public static partial class MyResultExtension
{
    //public static MyResult<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<T> candidates, Validate<T, MyError> validate) =>
    //    candidates
    //        .Select(c => c.Validate(validate))
    //        .Aggregate();

    //public static MyResult<IReadOnlyCollection<T>> AllOk<T>(this IEnumerable<MyResult<T>> candidates,
    //    Validate<T, MyError> validate) =>
    //    candidates
    //        .Bind(items => items.AllOk(validate));

    //public static MyResult<T> Validate<T>(this MyResult<T> item, Validate<T, MyError> validate, string? errorSeparator = null) => item.Bind(i => i.Validate(validate));

    //public static MyResult<T> Validate<T>(this T item, Validate<T, MyError> validate)
    //{
    //    var errors = MergeErrors(validate(item));
    //    return errors != null ? MyResult.Error<T>(errors) : item;
    //}

    //public static MyResult<T> FirstOk<T>(this IEnumerable<T> candidates, Validate<T, MyError> validate, Func<MyError> onEmpty) =>
    //    candidates
    //        .Select(r => r.Validate(validate))
    //        .FirstOk(onEmpty);
}