namespace FunicularSwitch.Generators.Transformer;

internal record MethodGenerationInfo(
    string ReturnType,
    IReadOnlyList<string> TypeParameters,
    IReadOnlyList<ParameterGenerationInfo> Parameters,
    string Name,
    MethodBody Body,
    bool IsAsync = false)
{
    public MethodGenerationInfo(string ReturnType,
        IReadOnlyList<string> TypeParameters,
        IReadOnlyList<ParameterGenerationInfo> Parameters,
        MethodInfo info) : this(
        ReturnType,
        TypeParameters,
        Parameters,
        info.Name,
        info.Invoke(TypeParameters, Parameters.Select(x => x.Name).ToList()))
    {
    }
    
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