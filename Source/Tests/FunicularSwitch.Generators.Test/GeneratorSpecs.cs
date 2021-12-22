using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Test
{
    [TestClass]
    public class Run_result_type_generator : VerifySourceGenerator
    {
        [TestMethod]
        public Task For_enum_error_type()
        {
            var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[ResultType(errorType: typeof(MyError))]
public abstract partial class OperationResult<T>
{
}

public enum MyError 
{
    Generic,
    NotFound,
    Unauthorized
}

//public static class MyErrorExtension
//{
//    [MergeError]
//    public static MyError MergeErrors(this MyError error, MyError other) => other;
//}
";
            return Verify(code);
        }


        [TestMethod]
        public Task For_internal_result_type()
        {
            var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[ResultType(errorType: typeof(MyError))]
abstract partial class OperationResult<T>
{
}

public enum MyError 
{
    Generic,
    NotFound,
    Unauthorized
}
";
            return Verify(code);
        }

        [TestMethod]
        public Task For_error_type_with_merge()
        {
            var code = @"
using FunicularSwitch.Generators;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FunicularSwitch.Test;

[ResultType(errorType: typeof(MyError))]
public abstract partial class OperationResult<T>
{
}

public abstract class MyError
{
    public static MyError Generic(string message) => new Generic_(message);

    public static readonly MyError NotFound = new NotFound_();
    public static readonly MyError NotAuthorized = new NotAuthorized_();

    public static MyError Aggregated(IEnumerable<MyError> errors) => new Aggregated_(errors);

    [MergeError]
    public MyError Merge(MyError other) => this is Aggregated_ a
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
";
            return Verify(code);
        }
    }
}