// ReSharper disable once CheckNamespace

using FunicularSwitch.Generators;

namespace FunicularSwitch.Generators.Consumer.System
{
    [ExtendedEnum]
    public enum MyEnum
    {
        One,
        Two
    }

    public class Action;

    [UnionType]
    public abstract partial record ArgumentException()
    {
        public record Func_() : ArgumentException;
        public record Action_() : ArgumentException;
    }

    [ResultType(typeof(Action))]
    public abstract partial class Result<T>;
}

namespace FunicularSwitch.Generators.Consumer
{
    [UnionType]
    public abstract partial record ArgumentException()
    {
        public record Func_() : ArgumentException;
        public record Action_() : ArgumentException;
    }
}

namespace MyNamespace
{
    [ResultType(ErrorType = typeof(MyNamespace2.ErrorInNamespaceWithDifferentResult))]
    public abstract partial class Result<T>;
}

namespace MyNamespace2
{
    public record ErrorInNamespaceWithDifferentResult();

    public class Result<T>;

    public static class MergeExtensions
    {
        [MergeError]
        public static ErrorInNamespaceWithDifferentResult Merge(this ErrorInNamespaceWithDifferentResult err1,
            // ReSharper disable once UnusedParameter.Global
            ErrorInNamespaceWithDifferentResult err2)
            => err1;
    }
}