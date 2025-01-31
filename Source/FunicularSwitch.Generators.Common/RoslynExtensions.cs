using System.Collections.Immutable;
using CommunityToolkit.Mvvm.SourceGenerators.Helpers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.Common;

public static class RoslynExtensions
{
    public static string? GetContainingNamespace(this SyntaxNode node)
    {
        var current = node;
        do
        {
            if (current is NamespaceDeclarationSyntax n)
                return n.Name.ToString();
            if (current is FileScopedNamespaceDeclarationSyntax f)
                return f.Name.ToString();
            current = current.Parent;
        } while (current != null);

        return null;
    }

    public static bool IsTypeDeclarationWithAttributes(this SyntaxNode syntaxNode) =>
        syntaxNode is BaseTypeDeclarationSyntax
        {
            AttributeLists.Count: > 0
        };

    public static bool IsAssemblyAttribute(this SyntaxNode s) =>
        s is AttributeSyntax
        {
            Parent: AttributeListSyntax l
        } && (l.Target?.Identifier.IsKind(SyntaxKind.AssemblyKeyword)).GetValueOrDefault();

    public static bool IsAnyKeyWord(this string identifier) =>
        SyntaxFacts.GetKeywordKind(identifier) != SyntaxKind.None
        || SyntaxFacts.GetContextualKeywordKind(identifier) != SyntaxKind.None
        || SyntaxFacts.GetReservedKeywordKinds().Contains(SyntaxFactory.ParseToken(identifier).Kind());


    public static bool InheritsFrom(this INamedTypeSymbol symbol, ITypeSymbol type)
    {
        var baseType = symbol.BaseType;
        while (baseType != null)
        {
            if (type.Equals(baseType, SymbolEqualityComparer.Default))
                return true;

            baseType = baseType.BaseType;
        }

        return false;
    }

    public static bool Implements(this INamedTypeSymbol symbol, ITypeSymbol interfaceType)
        => symbol.AllInterfaces.Any(i => interfaceType.Equals(i, SymbolEqualityComparer.Default));

    public static string? GetFullNamespace(this INamedTypeSymbol namedType)
    {
        var parentNamespaces = new List<string>();
        var current = namedType.ContainingNamespace;
        while (!current?.IsGlobalNamespace ?? false)
        {
            // ReSharper disable once RedundantSuppressNullableWarningExpression
            parentNamespaces.Add(current!.Name);
            current = current.ContainingNamespace;
        }

        if (parentNamespaces.Count == 0)
            return null;

        parentNamespaces.Reverse();
        return parentNamespaces.ToSeparatedString(".");
    }

    static readonly SymbolDisplayFormat FullTypeWithNamespaceDisplayFormat = SymbolWrapper.FullTypeWithNamespaceDisplayFormat;
    static readonly SymbolDisplayFormat FullTypeWithNamespaceAndGenericsDisplayFormat = SymbolWrapper.FullTypeWithNamespaceAndGenericsDisplayFormat;
    static readonly SymbolDisplayFormat FullTypeDisplayFormat = new(typeQualificationStyle: SymbolDisplayTypeQualificationStyle.NameAndContainingTypes);


    public static string FullTypeNameWithNamespace(this INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.ToDisplayString(FullTypeWithNamespaceDisplayFormat);
    public static string FullTypeNameWithNamespaceAndGenerics(this INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.ToDisplayString(FullTypeWithNamespaceAndGenericsDisplayFormat);
    public static string FullTypeName(this INamedTypeSymbol namedTypeSymbol) => namedTypeSymbol.ToDisplayString(FullTypeDisplayFormat);

    public static string FullNamespace(this INamespaceSymbol namespaceSymbol) =>
        namespaceSymbol.ToDisplayString(FullTypeDisplayFormat);

    public static QualifiedTypeName QualifiedName(this BaseTypeDeclarationSyntax dec)
    {
        var current = dec.Parent as BaseTypeDeclarationSyntax;
        var typeNames = new Stack<string>();
        while (current != null)
        {
            typeNames.Push(current.Name());
            current = current.Parent as BaseTypeDeclarationSyntax;
        }

        return new(dec.Name(), typeNames);
    }

    public static QualifiedTypeName QualifiedNameWithGenerics(this BaseTypeDeclarationSyntax dec)
    {
        var current = dec.Parent as BaseTypeDeclarationSyntax;
        var typeNames = new Stack<string>();
        while (current != null)
        {
            typeNames.Push(current.Name() + FormatTypeParameters(current.GetTypeParameterList()));
            current = current.Parent as BaseTypeDeclarationSyntax;
        }

        return new(dec.Name() + FormatTypeParameters(dec.GetTypeParameterList()), typeNames);
    }

    public static EquatableArray<string> GetTypeParameterList(this BaseTypeDeclarationSyntax dec)
    {
        if (dec is not TypeDeclarationSyntax tds)
        {
            return [];
        }

        return tds.TypeParameterList?.Parameters.Select(tps => tps.Identifier.Text).ToImmutableArray() ?? ImmutableArray<string>.Empty;
    }

    public static string FormatTypeParameters(EquatableArray<string> typeParameters)
    {
        if (typeParameters.Length == 0)
        {
            return string.Empty;
        }

        return "<" + string.Join(", ", typeParameters) + ">";
    }

    public static string FormatTypeParameterForFileName(EquatableArray<string> typeParameters)
    {
        if (typeParameters.Length == 0)
        {
            return string.Empty;
        }

        return "Of" + string.Join("_", typeParameters);
    }

    public static string Name(this BaseTypeDeclarationSyntax declaration) => declaration.Identifier.ToString();

    public static string Name(this MethodDeclarationSyntax declaration) => declaration.Identifier.ToString();

    public static string Name(this TypeSyntax typeSyntax) => typeSyntax.ToString();

    public static string Name(this PropertyDeclarationSyntax syntax) => syntax.Identifier.ToString();

    public static string? GetAttributeFullName(this AttributeSyntax attributeSyntax, SemanticModel semanticModel)
    {
        string? attributeFullName = null;
        var symbol = ModelExtensions.GetSymbolInfo(semanticModel, attributeSyntax).Symbol;
        if (symbol is IMethodSymbol attributeSymbol)
        {
            var attributeContainingTypeSymbol = attributeSymbol.ContainingType;
            attributeFullName = attributeContainingTypeSymbol.ToDisplayString();
        }

        return attributeFullName;
    }

    public static bool HasRequiredModifier(this SyntaxTokenList tokens) =>
        tokens.Any(token => token.Text == "required");

    public static bool HasModifier(this SyntaxTokenList tokens, SyntaxKind syntaxKind)
    {
        var token = SyntaxFactory.Token(syntaxKind).Text;
        return tokens.Any(t => t.Text == token);
    }

    public static bool HasModifier(this IEnumerable<string> tokens, SyntaxKind syntaxKind)
    {
        var token = SyntaxFactory.Token(syntaxKind).Text;
        return tokens.Contains(token);
    }

    public static string GetFullTypeName(this Compilation compilation, SyntaxNode typeSyntax)
    {
        var semanticModel = compilation.GetSemanticModel(typeSyntax.SyntaxTree);
        return GetFullTypeName(semanticModel, typeSyntax);
    }

    public static string GetFullTypeName(this SemanticModel semanticModel, SyntaxNode typeSyntax)
    {
        var typeInfo = semanticModel.GetTypeInfo(typeSyntax);
        return typeInfo.Type?.ToDisplayString(
            new SymbolDisplayFormat(
                typeQualificationStyle: SymbolWrapper.FullTypeWithNamespaceDisplayFormat.TypeQualificationStyle
            )
        ) ?? typeSyntax.ToString();
    }

    public static IEnumerable<INamedTypeSymbol> GetAllTypes(this INamespaceOrTypeSymbol root)
    {
        foreach (var symbol in root.GetMembers())
        {
            switch (symbol)
            {
                case INamespaceSymbol namespaceSymbol:
                    foreach (var nested in GetAllTypes(namespaceSymbol))
                    {
                        yield return nested;
                    }
                    break;
                case INamedTypeSymbol typeSymbol:
                    yield return typeSymbol;
                    foreach (var nested in GetAllTypes(typeSymbol))
                    {
                        yield return nested;
                    }
                    break;
            }
        }
    }

    public static CallableMemberInfo ToMemberInfo(this BaseMethodDeclarationSyntax member, string name, Compilation compilation)
    {
        var modifiers = member.Modifiers;
        return new(name,
            ToEquatableModifiers(modifiers),
            member.ParameterList.Parameters
                .Select(p =>
                    ToParameterInfo(p, compilation))
                .ToImmutableArray());
    }

    public static ImmutableArray<string> ToEquatableModifiers(this SyntaxTokenList modifiers) => modifiers.Select(m => m.Text).ToImmutableArray();

    public static ParameterInfo ToParameterInfo(this ParameterSyntax p, Compilation compilation) =>
        new(
            p.Identifier.Text,
            p.Modifiers.ToEquatableModifiers(),
            compilation.GetSemanticModel(p.SyntaxTree).GetTypeInfo(p.Type!).Type!,
            p.Default?.ToString());

    public static PropertyOrFieldInfo ToPropertyOrFieldInfo(this MemberDeclarationSyntax m, Compilation compilation)
    {
        var (memberName, type) = m switch
        {
            PropertyDeclarationSyntax p => (p.Name(), p.Type),
            FieldDeclarationSyntax f => (f.Declaration.Variables[0].Identifier.Text, f.Declaration.Type),
            _ => throw new InvalidOperationException($"Cannot extract parameter info from member of type {m.GetType()}")
        };

        return new(
            memberName,
            compilation.GetSemanticModel(m.SyntaxTree).GetTypeInfo(type).Type!);
    }


    public static IEnumerable<string> GetUnusedNames(this ImmutableList<string> usedNames,
        IEnumerable<string> preferredNames) =>
        preferredNames
            .Aggregate(usedNames, (names, preferredName) => names.Add(names.GetUnusedName(preferredName)))
            .Skip(usedNames.Count);

    public static string GetUnusedName(this IReadOnlyCollection<string> usedNames, string preferredName, string? secondChoiceAndPrefix = null)
    {
        return Candidates()
            .Select(Check)
            .First(s => s is not null)!;

        IEnumerable<string> Candidates()
        {
            if (secondChoiceAndPrefix != null)
                yield return preferredName;

            var postfix = secondChoiceAndPrefix ?? preferredName;
            foreach (var i in Enumerable.Range(0, 20))
                yield return new string('_', i) + postfix;

            yield return postfix + Guid.NewGuid().ToString("N");
        }

        string? Check(string typeName) => !usedNames.Contains(typeName) ? typeName : null;
    }
}

public sealed class QualifiedTypeName : IEquatable<QualifiedTypeName>
{
    public static QualifiedTypeName NoParents(string name) => new(name, []);

    readonly string m_FullName;
    public ImmutableArray<string> NestingParents { get; }
    public string Name { get; }

    public string QualifiedName(string separator = ".") => NestingParents.Length > 0 ? $"{NestingParents.ToSeparatedString(separator)}{separator}{Name}" : Name;

    public QualifiedTypeName(string name, IEnumerable<string> nestingParents)
    {
        Name = name;
        NestingParents = nestingParents.ToImmutableArray();
        m_FullName = QualifiedName(".");
    }

    public override string ToString() => m_FullName;

    public bool Equals(QualifiedTypeName? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return m_FullName == other.m_FullName;
    }

    public override bool Equals(object? obj) => ReferenceEquals(this, obj) || obj is QualifiedTypeName other && Equals(other);

    public override int GetHashCode() => m_FullName.GetHashCode();

    public static bool operator ==(QualifiedTypeName left, QualifiedTypeName right) => Equals(left, right);

    public static bool operator !=(QualifiedTypeName left, QualifiedTypeName right) => !Equals(left, right);
}

public sealed record CallableMemberInfo(string Name, EquatableArray<string> Modifiers, EquatableArray<ParameterInfo> Parameters);

public sealed record PropertyOrFieldInfo(string Name, ITypeSymbol Type);

public sealed record ParameterInfo
{
    readonly string _typeName;

    public string Name { get; }
    public EquatableArray<string> Modifiers { get; }
    public ITypeSymbol Type { get; }
    public string? DefaultClause { get; }

    public ParameterInfo(string name, EquatableArray<string> modifiers, ITypeSymbol type, string? defaultClause)
    {
        Name = name;
        Modifiers = modifiers;
        Type = type;
        DefaultClause = defaultClause;

        _typeName = Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat);
    }

    public override string ToString()
    {
        return Parts().ToSeparatedString(" ");

        IEnumerable<string> Parts()
        {
            if (Modifiers.Length > 0)
                yield return Modifiers.ToSeparatedString(" ");
            yield return _typeName;
            yield return Name;
            if (DefaultClause != null)
                yield return DefaultClause;
        }
    }

    public bool Equals(ParameterInfo? other)
    {
        if (ReferenceEquals(null, other)) return false;
        if (ReferenceEquals(this, other)) return true;
        return _typeName == other._typeName && Name == other.Name && Modifiers.Equals(other.Modifiers) && DefaultClause == other.DefaultClause;
    }

    public override int GetHashCode()
    {
        unchecked
        {
            var hashCode = _typeName.GetHashCode();
            hashCode = (hashCode * 397) ^ Name.GetHashCode();
            hashCode = (hashCode * 397) ^ Modifiers.GetHashCode();
            hashCode = (hashCode * 397) ^ (DefaultClause != null ? DefaultClause.GetHashCode() : 0);
            return hashCode;
        }
    }
}