using AwesomeAssertions.Execution;

namespace FunicularSwitch.Generic.Assertions;

public static class GenericResultAssertionsExtensions
{
    public static GenericResultAssertions<TOk, TError> Should<TOk, TError>(this GenRes<TOk, TError> instance)
    {
        return new GenericResultAssertions<TOk, TError>(instance, AssertionChain.GetOrCreate());
    }
}