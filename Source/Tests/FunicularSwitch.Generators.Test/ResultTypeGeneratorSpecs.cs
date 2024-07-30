using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_result_type_generator : VerifySourceGenerator
{
    [TestMethod]
    public Task For_enum_error_type()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

public class OtherAttribute : System.Attribute
{
}

[Other]
[ResultType(ErrorType = typeof(MyError))]
public abstract partial class OperationResult<T>
{
}

public enum MyError 
{
    Generic,
    NotFound,
    Unauthorized
}

[ResultType(errorType: typeof(string))]
public abstract partial class Result<T>
{
}

public static class MyErrorExtension
{
    [Other]
    [MergeError]
    public static MyError MergeErrors(this MyError error, MyError other) => other;

    [MergeError]
    public static string MergeErrors(this string error, string other) => $""{error}{System.Environment.NewLine}{other}"";
}
";
        return Verify(code);
    }

    [TestMethod]
    public Task For_error_type_in_different_namespace()
    {
        var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test
{
    [ResultType(errorType: typeof(FunicularSwitch.Test.Errors.MyError))]
    public abstract partial class OperationResult<T>
    {
    }
}

namespace FunicularSwitch.Test.Errors
{
    public enum MyError 
    {
        Generic,
        NotFound,
        Unauthorized
    }
}

namespace FunicularSwitch.Test.Extensions
{
    public static class MyErrorExtension
    {
        [MergeError]
        public static FunicularSwitch.Test.Errors.MyError MergeErrors(this FunicularSwitch.Test.Errors.MyError error, FunicularSwitch.Test.Errors.MyError other) => other;
    }
}
";
        return Verify(code);
    }

    [TestMethod]
    public Task For_result_type_without_namespace()
    {
	    var code = @"
using FunicularSwitch.Generators;

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

public class OtherAttribute : System.Attribute
{
}

[ResultType(errorType: typeof(MyError))]
public abstract partial class OperationResult<T>
{
}

public static class ErrorFactory
{
    [Other]
	[ExceptionToError]
	public static MyError FromException(Exception e) => MyError.Generic(e.ToString());
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