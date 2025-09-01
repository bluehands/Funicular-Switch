namespace FunicularSwitch.Generators.Transformer;

internal record MethodGenerationInfo(
    TypeInfo ReturnType,
    IReadOnlyList<string> TypeParameters,
    IReadOnlyList<ParameterGenerationInfo> Parameters,
    string Name,
    MethodBody Body,
    bool IsAsync = false)
{
    public class SignatureComparer : IEqualityComparer<MethodGenerationInfo>
    {
        public static SignatureComparer Instance { get; } = new();

        public bool Equals(MethodGenerationInfo? x, MethodGenerationInfo? y)
        {
            if (ReferenceEquals(x, y)) return true;
            if (x is null) return false;
            if (y is null) return false;
            if (x.GetType() != y.GetType()) return false;
            return x.Name == y.Name
                   && x.Parameters
                       .Select(x => x.Type)
                       .SequenceEqual(y.Parameters
                           .Select(x => x.Type));
        }

        public int GetHashCode(MethodGenerationInfo obj) =>
            obj.Name.GetHashCode();
    }
}
