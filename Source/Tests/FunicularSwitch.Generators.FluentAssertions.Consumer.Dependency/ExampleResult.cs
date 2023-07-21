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
