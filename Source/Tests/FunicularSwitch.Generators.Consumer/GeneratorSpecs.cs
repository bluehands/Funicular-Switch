using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Globalization;
using System.Threading.Tasks;
using FluentAssertions;
using FunicularSwitch.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Consumer
{
    [TestClass]
    public class When_using_generated_result_type
    {
        [TestMethod]
        public void Then_it_feels_good()
        {
            static OperationResult<decimal> Divide(decimal i, decimal divisor) => divisor == 0
                ? OperationResult.Error<decimal>(MyError.Generic("Division by zero"))
                : i / divisor;

            OperationResult<int> result = 42;

            var calc = result
                .Bind(i => Divide(i, 0))
                .Map(i => (i * 2).ToString(CultureInfo.InvariantCulture));

            calc.Should().BeEquivalentTo(OperationResult<string>.Error(MyError.Generic("Division by zero")));

            var combinedError = calc.Aggregate(OperationResult.Error<int>(MyError.NotFound));
            var combinedErrorStatic = OperationResult.Aggregate(calc, OperationResult.Error<int>(MyError.NotFound), (_, i) => i);
            var combinedOk = OperationResult.Ok(42).Aggregate(OperationResult.Ok(" is the answer"));
            var combinedOkStatic = OperationResult.Aggregate(OperationResult.Ok(42), OperationResult.Ok(" is the answer"));

            static IEnumerable<MyError> IsGreaterThanFive(int i)
            {
                if (i <= 5)
                    yield return MyError.Generic("To small");
                if (i == 3)
                    yield return MyError.Generic("Uuh, it's three...");
            }

            (3.Validate(IsGreaterThanFive) is OperationResult<int>.Error_
            {
                Details: MyError.Aggregated_
                {
                    Errors:
                    {
                        Count: 2
                    }
                }
            }).Should().BeTrue();
        }

        [TestMethod]
        public void Then_assertion()
        {
            
        }
    }

    [ResultType(typeof(string))]
    public abstract partial class Result<T>
    {
    }

    [ResultType(typeof(MyError))]
    abstract partial class OperationResult<T>
    {
    }

    public static partial class MyErrorExtension
    {
        [MergeError]
        public static MyError MergeErrors(this MyError error, MyError other) => error.Merge(other);

        [MergeError]
        public static string MergeErrors(this string error, string other) => $"{error}{Environment.NewLine}{other}";
    }

    public abstract class MyError
    {
        public static MyError Generic(string message) => new Generic_(message);

        public static readonly MyError NotFound = new NotFound_();
        public static readonly MyError NotAuthorized = new NotAuthorized_();

        public static MyError Aggregated(ImmutableList<MyError> errors) => new Aggregated_(errors);

        public MyError Merge(MyError other) => this is Aggregated_ a
            ? a.Add(other)
            : other is Aggregated_ oa
                ? oa.Add(this)
                : Aggregated(ImmutableList.Create(this, other));

        public class Generic_ : MyError
        {
            public string Message { get; }

            public Generic_(string message) : base(UnionCases.Generic)
            {
                Message = message;
            }
        }

        public class NotFound_ : MyError
        {
            public NotFound_() : base(UnionCases.NotFound)
            {
            }
        }

        public class NotAuthorized_ : MyError
        {
            public NotAuthorized_() : base(UnionCases.NotAuthorized)
            {
            }
        }

        public class Aggregated_ : MyError
        {
            public ImmutableList<MyError> Errors { get; }

            public Aggregated_(ImmutableList<MyError> errors) : base(UnionCases.Aggregated) => Errors = errors;

            public MyError Add(MyError other) => Aggregated(Errors.Add(other));
        }

        internal enum UnionCases
        {
            Generic,
            NotFound,
            NotAuthorized,
            Aggregated
        }

        internal UnionCases UnionCase { get; }
        MyError(UnionCases unionCase) => UnionCase = unionCase;

        public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
        bool Equals(MyError other) => UnionCase == other.UnionCase;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MyError)obj);
        }

        public override int GetHashCode() => (int)UnionCase;
    }

    public static partial class MyErrorExtension
    {
        public static T Match<T>(this MyError myError, Func<MyError.Generic_, T> generic, Func<MyError.NotFound_, T> notFound, Func<MyError.NotAuthorized_, T> notAuthorized, Func<MyError.Aggregated_, T> aggregated)
        {
            switch (myError.UnionCase)
            {
                case MyError.UnionCases.Generic:
                    return generic((MyError.Generic_)myError);
                case MyError.UnionCases.NotFound:
                    return notFound((MyError.NotFound_)myError);
                case MyError.UnionCases.NotAuthorized:
                    return notAuthorized((MyError.NotAuthorized_)myError);
                case MyError.UnionCases.Aggregated:
                    return aggregated((MyError.Aggregated_)myError);
                default:
                    throw new ArgumentException($"Unknown type derived from MyError: {myError.GetType().Name}");
            }
        }

        public static async Task<T> Match<T>(this MyError myError, Func<MyError.Generic_, Task<T>> generic, Func<MyError.NotFound_, Task<T>> notFound, Func<MyError.NotAuthorized_, Task<T>> notAuthorized, Func<MyError.Aggregated_, Task<T>> aggregated)
        {
            switch (myError.UnionCase)
            {
                case MyError.UnionCases.Generic:
                    return await generic((MyError.Generic_)myError).ConfigureAwait(false);
                case MyError.UnionCases.NotFound:
                    return await notFound((MyError.NotFound_)myError).ConfigureAwait(false);
                case MyError.UnionCases.NotAuthorized:
                    return await notAuthorized((MyError.NotAuthorized_)myError).ConfigureAwait(false);
                case MyError.UnionCases.Aggregated:
                    return await aggregated((MyError.Aggregated_)myError).ConfigureAwait(false);
                default:
                    throw new ArgumentException($"Unknown type derived from MyError: {myError.GetType().Name}");
            }
        }

        public static async Task<T> Match<T>(this Task<MyError> myError, Func<MyError.Generic_, T> generic, Func<MyError.NotFound_, T> notFound, Func<MyError.NotAuthorized_, T> notAuthorized, Func<MyError.Aggregated_, T> aggregated) => (await myError.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated);
        public static async Task<T> Match<T>(this Task<MyError> myError, Func<MyError.Generic_, Task<T>> generic, Func<MyError.NotFound_, Task<T>> notFound, Func<MyError.NotAuthorized_, Task<T>> notAuthorized, Func<MyError.Aggregated_, Task<T>> aggregated) => await (await myError.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
    }
}