namespace FunicularSwitch.Generators.Consumer;

[UnionType]
public abstract partial class WithPrimaryConstructor(int test)
{
    public int Test { get; } = test;
}

public class DerivedWithPrimaryConstructor(string message, int test) : WithPrimaryConstructor(test)
{
    public string Message { get; } = message;
}

[UnionType] //if you are on .net standard use StaticFactoryMethods=false because default interface implementations are not supported
public partial interface IUnion
{
    public record Case1_ : IUnion;
    public record Case2_ : IUnion;
}

[UnionType]
public abstract partial record Failure
{
    public record NotFound_(int Id) : Failure;
}

public record InvalidInputFailure(string Message) : Failure;

[FunicularSwitch.Generators.UnionType]
abstract partial record CardType //declare partial to allow source generator to add static factory methods
{
    public record MaleCardType(int Age) : CardType;
    public record FemaleCardType : CardType;
}

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial record GenericResult<T, TFailure>(bool IsOk)
{
    public record Ok_(T Value) : GenericResult<T, TFailure>(true);
    public record Error_(TFailure Failure) : GenericResult<T, TFailure>(false);
}