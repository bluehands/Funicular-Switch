using System.Collections.Immutable;
using System;

namespace FunicularSwitch.Generators.Consumer;

[ResultType(ErrorType = typeof(string))]
public abstract partial class Result<T>
{
}

[ResultType(typeof(Error))]
abstract partial class OperationResult<T>
{
}

public static partial class ErrorExtension
{
    [MergeError]
    public static Error MergeErrors(this Error error, Error other) => error.Merge(other);

    [MergeError]
    public static string MergeErrors(this string error, string other) => $"{error}{Environment.NewLine}{other}";

    [ExceptionToError]
    public static string UnexpectedToStringError(Exception exception) => $"Unexpected error occurred: {exception}";
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial class Error
{
    [ExceptionToError]
    public static Error Generic(Exception exception) => Generic(exception.ToString());

    public Error Merge(Error other) => this is Aggregated_ a
        ? a.Add(other)
        : other is Aggregated_ oa
            ? oa.Add(this)
            : Aggregated(ImmutableList.Create(this, other));

    public class Generic_ : Error
    {
        public string Message { get; }

        public Generic_(string message) : base(UnionCases.Generic)
        {
            Message = message;
        }
    }

    public class NotFound_ : Error
    {
        public NotFound_() : base(UnionCases.NotFound)
        {
        }
    }

    public class NotAuthorized_ : Error
    {
        public NotAuthorized_() : base(UnionCases.NotAuthorized)
        {
        }
    }

    public class Aggregated_ : Error
    {
        public ImmutableList<Error> Errors { get; }

        public Aggregated_(ImmutableList<Error> errors) : base(UnionCases.Aggregated) => Errors = errors;

        public Error Add(Error other) => Aggregated(Errors.Add(other));
    }

    internal enum UnionCases
    {
        Generic,
        NotFound,
        NotAuthorized,
        Aggregated
    }

    internal UnionCases UnionCase { get; }
    Error(UnionCases unionCase) => UnionCase = unionCase;

    public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
    bool Equals(Error other) => UnionCase == other.UnionCase;

    public override bool Equals(object? obj)
    {
        if (ReferenceEquals(null, obj)) return false;
        if (ReferenceEquals(this, obj)) return true;
        if (obj.GetType() != GetType()) return false;
        return Equals((Error)obj);
    }

    public override int GetHashCode() => (int)UnionCase;
}