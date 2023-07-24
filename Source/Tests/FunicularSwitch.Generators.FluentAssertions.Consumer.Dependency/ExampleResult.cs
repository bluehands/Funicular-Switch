namespace FunicularSwitch.Generators.FluentAssertions.Consumer.Dependency;

[ResultType(ErrorType = typeof(MyError))]
public partial class ExampleResult
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
    }

    public sealed class SecondCase_ : MyError
    {
        public SecondCase_(string text)
        {
            Text = text;
        }

        public string Text { get; }
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