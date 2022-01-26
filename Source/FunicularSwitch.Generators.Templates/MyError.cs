using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FunicularSwitch.Generators.Templates
{
#pragma warning disable 1591
    public abstract class MyError
    {
        public static MyError Generic(string message) => new Generic_(message);

        public static readonly MyError NotFound = new NotFound_();
        public static readonly MyError NotAuthorized = new NotAuthorized_();

        public static MyError Aggregated(IEnumerable<MyError> errors) => new Aggregated_(errors);

        public MyError Merge__MemberOrExtensionMethod(MyError other) => this is Aggregated_ a
            ? a.Add(other)
            : other is Aggregated_ oa
                ? oa.Add(this)
                : Aggregated(new []{this, other});

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
            public List<MyError> Errors { get; }

            public Aggregated_(IEnumerable<MyError> errors) : base(UnionCases.Aggregated) => Errors = errors.ToList();

            public MyError Add(MyError other) => Aggregated(Errors.Concat(new []{other}));
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

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((MyError)obj);
        }

        public override int GetHashCode() => (int)UnionCase;
    }

    public static class MyErrorExtension
    {
        public static T Match<T>(this MyError myError, Func<MyError.Generic_, T> generic,
            Func<MyError.NotFound_, T> notFound, Func<MyError.NotAuthorized_, T> notAuthorized,
            Func<MyError.Aggregated_, T> aggregated)
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

        public static async Task<T> Match<T>(this MyError myError, Func<MyError.Generic_, Task<T>> generic,
            Func<MyError.NotFound_, Task<T>> notFound, Func<MyError.NotAuthorized_, Task<T>> notAuthorized,
            Func<MyError.Aggregated_, Task<T>> aggregated)
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

        public static async Task<T> Match<T>(this Task<MyError> myError, Func<MyError.Generic_, T> generic,
            Func<MyError.NotFound_, T> notFound, Func<MyError.NotAuthorized_, T> notAuthorized,
            Func<MyError.Aggregated_, T> aggregated) =>
            (await myError.ConfigureAwait(false)).Match(generic, notFound, notAuthorized, aggregated);

        public static async Task<T> Match<T>(this Task<MyError> myError, Func<MyError.Generic_, Task<T>> generic,
            Func<MyError.NotFound_, Task<T>> notFound, Func<MyError.NotAuthorized_, Task<T>> notAuthorized,
            Func<MyError.Aggregated_, Task<T>> aggregated) => await (await myError.ConfigureAwait(false))
            .Match(generic, notFound, notAuthorized, aggregated).ConfigureAwait(false);
    }
#pragma warning restore 1591
}