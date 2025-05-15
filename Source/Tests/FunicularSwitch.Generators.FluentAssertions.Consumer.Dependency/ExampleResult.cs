using System.Collections.Generic;

namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

[ResultType(ErrorType = typeof(MyError))]
public partial class ExampleResult
{
}

[ResultType(ErrorType = typeof(object))]
public partial class GenericResult<T>
{
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial class MyError
{
    public static MyError FirstCase(int number) => new FirstCase_(number);

    public static MyError SecondCase(string text) => new SecondCase_(text);

    public sealed class FirstCase_ : MyError
    {
        public FirstCase_(int number)
        {
            Number = number;
        }

        public int Number { get; }

        public override string ToString()
        {
            return $"{nameof(FirstCase)}: {nameof(this.Number)} = {this.Number}";
        }
    }

    public sealed class SecondCase_ : MyError
    {
        public SecondCase_(string text)
        {
            Text = text;
        }

        public string Text { get; }

        public override string ToString()
        {
            return $"{nameof(SecondCase)}: {nameof(this.Text)} = {this.Text}";
        }
    }
}

public class WrapperClass
{
    [UnionType(CaseOrder = CaseOrder.AsDeclared)]
    public abstract partial record NestedUnionType
    {
        public static NestedUnionType Derived(int number) => new DerivedNestedUnionType_(number);
        public static NestedUnionType Other(string message) => new OtherDerivedNestedUnionType_(message);

        public sealed record DerivedNestedUnionType_(int Number) : NestedUnionType;
        public sealed record OtherDerivedNestedUnionType_(string Message) : NestedUnionType;
    }
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
internal abstract record InternalUnionType
{
    public sealed record First(string Text) : InternalUnionType;
    public sealed record Second(string Text) : InternalUnionType;
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial record GenericUnionType<T>
{
    public sealed record First_(T Value) : GenericUnionType<T>;

    public sealed record Second_ : GenericUnionType<T>;
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial record MultiGenericUnionType<TFirst, TSecond, TThird>
{
    public sealed record One_(TFirst First, TSecond Second) : MultiGenericUnionType<TFirst, TSecond, TThird>;

    public sealed record Two_(TThird Third) : MultiGenericUnionType<TFirst, TSecond, TThird>;
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial record GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>
    where TFirst : struct, IEnumerable<int>
    where TSecond : class, new()
    where TThird : WrapperClass
    where TFourth : notnull
{
    public sealed record Derived : GenericUnionTypeWithConstraints<TFirst, TSecond, TThird, TFourth>;
}