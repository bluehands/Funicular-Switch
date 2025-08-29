namespace FunicularSwitch.Generators.Transformer;

internal record MethodGenerationInfo(
    string ReturnType,
    IReadOnlyList<string> TypeParameters,
    IReadOnlyList<ParameterGenerationInfo> Parameters,
    string Name,
    MethodBody Body,
    bool IsAsync = false)
{
    public class Comparer : IEqualityComparer<MethodGenerationInfo>
    {
        public static Comparer Instance { get; } = new();

        public bool Equals(MethodGenerationInfo? x, MethodGenerationInfo? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.ReturnType == y.ReturnType
                   && x.Name == y.Name
                   && x.Body.Equals(y.Body)
                   && x.Parameters.SequenceEqual(y.Parameters);
        }

        public int GetHashCode(MethodGenerationInfo obj) =>
            obj.Name.GetHashCode() ^ obj.Body.GetHashCode() ^ obj.ReturnType.GetHashCode();
    }
}
