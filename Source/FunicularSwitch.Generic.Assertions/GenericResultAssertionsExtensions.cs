using AwesomeAssertions.Execution;

namespace FunicularSwitch.Generic.Assertions;

public static class GenericResultAssertionsExtensions
{
    public static GenericResultAssertions<TOk, TError> Should<TOk, TError>(this GenericResult<TOk, TError> instance)
    {
        return new GenericResultAssertions<TOk, TError>(instance, AssertionChain.GetOrCreate());
    }
}