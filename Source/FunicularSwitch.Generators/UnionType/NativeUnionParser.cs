using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.UnionType;

/// <summary>Builds the schema for C# 15 <c>union</c> declarations. Everything is derived from the symbol, cases are the union case types in declared order.</summary>
static class NativeUnionParser
{
    public static GenerationResult<UnionTypeSchema> GetSchema(INamedTypeSymbol unionSymbol, Location location, bool forceInternal = false)
    {
        var acc = unionSymbol.GetActualAccessibility();
        if (acc is Accessibility.Private or Accessibility.Protected)
        {
            return GenerationResult<UnionTypeSchema>.Empty.AddDiagnostics(
                Diagnostics.UnionTypeIsNotAccessible($"{unionSymbol.FullTypeNameWithNamespace()} needs at least internal accessibility", location));
        }

        //UnionCaseTypes carries no nullable annotations, creation constructors do
        var creationParameterTypes = unionSymbol.Constructors
            .Where(c => c is { Parameters.Length: 1 } && c.DeclaredAccessibility is Accessibility.Public or Accessibility.Internal)
            .Select(c => c.Parameters[0].Type)
            //union pattern with static Create methods declared on the union or on a nested IUnionMembers interface
            .Concat(unionSymbol.GetMembers().OfType<INamedTypeSymbol>().Where(t => t.Name == "IUnionMembers").Concat([unionSymbol])
                .SelectMany(t => t.GetMembers("Create").OfType<IMethodSymbol>())
                .Where(m => m is { IsStatic: true, Parameters.Length: 1 })
                .Select(m => m.Parameters[0].Type))
            .ToImmutableArray();
        var caseTypes = unionSymbol.UnionCaseTypes
            .Select(t => creationParameterTypes.FirstOrDefault(p =>
                SymbolEqualityComparer.Default.Equals(p.WithNullableAnnotation(NullableAnnotation.NotAnnotated), t.WithNullableAnnotation(NullableAnnotation.NotAnnotated))) ?? t)
            .ToImmutableArray();
        var caseSymbols = caseTypes.Select(t => t.WithNullableAnnotation(NullableAnnotation.NotAnnotated)).ToImmutableArray();

        var usedNames = ImmutableList<string>.Empty.Add(unionSymbol.Name.ToParameterName().TrimStart('@'));
        var cases = ImmutableArray<DerivedType>.Empty;
        for (var i = 0; i < caseTypes.Length; i++)
        {
            var caseType = caseTypes[i];
            var (patternType, isNullable) = GetPatternType(caseType);

            var name = DeriveParameterName(patternType);
            if (usedNames.Contains(name))
                name = $"{name}{i + 1}";
            usedNames = usedNames.Add(name);

            var rank = caseSymbols.Count(other => !SymbolEqualityComparer.Default.Equals(other, caseSymbols[i])
                && caseSymbols[i] is INamedTypeSymbol derived && (derived.InheritsFrom(other) || derived.Implements(other)));

            cases = cases.Add(new DerivedType(
                fullTypeName: patternType.ToDisplayString(SymbolWrapper.FullTypeWithNamespaceAndGenericsDisplayFormat),
                parameterName: name.PrefixAtIfKeyword(),
                staticFactoryMethodName: name.FirstToUpper().PrefixAtIfKeyword(),
                handlerTypeName: caseType.ToDisplayString(SymbolWrapper.FullTypeWithNamespaceAndGenericsDisplayFormat),
                isNullableCase: isNullable,
                armRank: rank));
        }

        return new UnionTypeSchema(
            Namespace: unionSymbol.GetFullNamespace(),
            TypeName: unionSymbol.Name,
            FullTypeName: unionSymbol.FullTypeNameWithNamespace(),
            FullTypeNameWithTypeParameters: unionSymbol.FullTypeNameWithNamespaceAndGenerics(),
            Cases: cases,
            TypeParameters: unionSymbol.TypeParameters.Select(p => p.Name).ToImmutableArray(),
            TypeConstraints: unionSymbol.TypeParameters
                .Select(p => p.FormatTypeConstraints().Trim())
                .Where(c => c.Length > 0)
                .ToImmutableArray(),
            IsInternal: forceInternal || acc is Accessibility.NotApplicable or Accessibility.Internal,
            IsPartial: false,
            IsExhaustive: true,
            IsNativeUnion: true,
            TypeKind: UnionTypeTypeKind.Class,
            Modifiers: ImmutableArray<string>.Empty,
            StaticFactoryInfo: null);
    }

    /// <summary>Type to use in patterns (nullable annotations are not allowed there) and whether the case admits null.</summary>
    static (ITypeSymbol patternType, bool isNullable) GetPatternType(ITypeSymbol caseType)
    {
        if (caseType is INamedTypeSymbol { OriginalDefinition.SpecialType: SpecialType.System_Nullable_T } nullableValueType)
            return (nullableValueType.TypeArguments[0], true);

        if (caseType.IsReferenceType && caseType.NullableAnnotation == NullableAnnotation.Annotated)
            return (caseType.WithNullableAnnotation(NullableAnnotation.NotAnnotated), true);

        return (caseType, false);
    }

    static string DeriveParameterName(ITypeSymbol type)
    {
        var name = type switch
        {
            IArrayTypeSymbol array => DeriveParameterName(array.ElementType) + "Array",
            _ => type.SpecialType != SpecialType.None
                ? type.ToDisplayString(SymbolDisplayFormat.MinimallyQualifiedFormat) //int, string, ...
                : type.Name
        };
        name = name.Trim('@', '_');
        if (name.Length == 0 || !char.IsLetter(name[0]))
            name = "case" + name;
        return name.FirstToLower();
    }
}
