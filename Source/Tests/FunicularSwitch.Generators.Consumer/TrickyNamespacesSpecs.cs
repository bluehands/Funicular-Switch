// ReSharper disable once CheckNamespace
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