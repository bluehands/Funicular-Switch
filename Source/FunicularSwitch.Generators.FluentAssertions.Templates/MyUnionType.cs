namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    [UnionType(CaseOrder = CaseOrder.AsDeclared)]
    public abstract partial record MyUnionType
    {
    }
    public sealed record MyDerivedUnionType : MyUnionType;
}