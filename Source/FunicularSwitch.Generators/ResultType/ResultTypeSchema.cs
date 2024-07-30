using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.ResultType;

sealed class ResultTypeSchema(
    ClassDeclarationSyntax resultType,
    INamedTypeSymbol? errorType)
{
    public SymbolWrapper<INamedTypeSymbol>? ErrorType { get; } = errorType != null ? new (errorType) : null;
    public LocationInfo? ResultTypeLocation { get; } = LocationInfo.CreateFrom(resultType.GetLocation());
    public bool IsInternal { get; } = !resultType.Modifiers.HasModifier(SyntaxKind.PublicKeyword);
    public QualifiedTypeName ResultTypeName { get; } = resultType.QualifiedName();
    public string? ResultTypeNamespace { get; } = resultType.GetContainingNamespace();

    bool Equals(ResultTypeSchema other) => Equals(ErrorType, other.ErrorType) && IsInternal == other.IsInternal && ResultTypeName == other.ResultTypeName && ResultTypeNamespace == other.ResultTypeNamespace;

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is ResultTypeSchema other && Equals(other);

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = ErrorType?.GetHashCode() ?? 0;
            hashCode = (hashCode * 397) ^ IsInternal.GetHashCode();
            hashCode = (hashCode * 397) ^ ResultTypeName.GetHashCode();
            hashCode = (hashCode * 397) ^ (ResultTypeNamespace != null ? ResultTypeNamespace.GetHashCode() : 0);
            return hashCode;
        }
    }

    public static bool operator ==(ResultTypeSchema? left, ResultTypeSchema? right) => Equals(left, right);

    public static bool operator !=(ResultTypeSchema? left, ResultTypeSchema? right) => !Equals(left, right);

    public override string ToString() => $"{nameof(ResultType)}: {ResultTypeName}, {nameof(ErrorType)}: {ErrorType}";
}