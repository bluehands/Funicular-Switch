//HintName: GlobalNamespaceTypesConsumer.ResultWithGlobalErrorAwesomeAssertionExtensions.g.cs
#nullable enable
using GlobalNamespaceTypesConsumer;

namespace GlobalNamespaceTypesConsumer
{
    internal static class ResultWithGlobalErrorAwesomeAssertionExtensions
    {
        public static ResultWithGlobalErrorAssertions<T> Should<T>(this global::GlobalNamespaceTypesConsumer.ResultWithGlobalError<T> result) => new(result);
    }
}