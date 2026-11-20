using FunicularSwitch.Generators;

// intentionally in the global namespace
public record GlobalError(string Message);

[UnionType]
public abstract partial record GlobalUnion;

public record GlobalCaseOne : GlobalUnion;

public record GlobalCaseTwo(int Value) : GlobalUnion;

namespace GlobalNamespaceTypesConsumer
{
    [ResultType(ErrorType = typeof(GlobalError))]
    public abstract partial class ResultWithGlobalError<T>
    {
    }
}
