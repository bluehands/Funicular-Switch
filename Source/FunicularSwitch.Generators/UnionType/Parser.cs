using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.UnionType;

static class Parser
{
    public static GenerationResult<UnionTypeSchema> GetUnionTypeSchema(Compilation compilation,
        CancellationToken cancellationToken,
        BaseTypeDeclarationSyntax unionTypeClass,
        INamedTypeSymbol unionTypeSymbol,
        AttributeData unionTypeAttribute)
    {
        var semanticModel = compilation.GetSemanticModel(unionTypeClass.SyntaxTree);

        var typeParameters = unionTypeClass.GetTypeParameterList();

        var fullTypeName = unionTypeSymbol.FullTypeNameWithNamespace();
        var fullTypeNameWithTypeParameters = fullTypeName + RoslynExtensions.FormatTypeParameters(typeParameters);
        var acc = unionTypeSymbol.GetActualAccessibility();
        if (acc is Accessibility.Private or Accessibility.Protected)
        {
            var diag = Diagnostics.UnionTypeIsNotAccessible($"{fullTypeName} needs at least internal accessibility", unionTypeClass.GetLocation());
            return Error(diag);
        }

        var caseOrder = unionTypeAttribute.GetEnumNamedArgument("CaseOrder", CaseOrder.Alphabetic);
        var staticFactoryMethods = unionTypeAttribute.GetNamedArgument("StaticFactoryMethods", true);


        var fullNamespace = unionTypeSymbol.GetFullNamespace();

        var derivedTypes = compilation.SyntaxTrees.SelectMany(syntaxTree =>
        {
            var root = syntaxTree.GetRoot(cancellationToken);
            var treeSemanticModel = syntaxTree != unionTypeClass.SyntaxTree ? compilation.GetSemanticModel(syntaxTree) : semanticModel;

            return FindConcreteDerivedTypesWalker.Get(root, unionTypeSymbol, treeSemanticModel);
        });


        var isPartial = unionTypeClass.Modifiers.HasModifier(SyntaxKind.PartialKeyword)
            && unionTypeSymbol.ContainingType == null; //for now do not generate factory methods for nested types, we could support that if all containing types are partial
        var generateFactoryMethods = isPartial && staticFactoryMethods;

        return
            ToOrderedCases(caseOrder, derivedTypes, compilation, generateFactoryMethods, unionTypeSymbol.Name)
                .Map(cases => new UnionTypeSchema(
                    Namespace: fullNamespace,
                    TypeName: unionTypeSymbol.Name,
                    FullTypeName: fullTypeName,
                    FullTypeNameWithTypeParameters: fullTypeNameWithTypeParameters,
                    Cases: cases,
                    TypeParameters: typeParameters,
                    IsInternal: acc is Accessibility.NotApplicable or Accessibility.Internal,
                    IsPartial: isPartial,
                    TypeKind: unionTypeClass switch
                    {
                        RecordDeclarationSyntax => UnionTypeTypeKind.Record,
                        InterfaceDeclarationSyntax => UnionTypeTypeKind.Interface,
                        _ => UnionTypeTypeKind.Class
                    },
                    Modifiers: unionTypeClass.Modifiers.ToEquatableModifiers(),
                    StaticFactoryInfo: generateFactoryMethods
                        ? BuildFactoryInfo(unionTypeClass, compilation)
                        : null
                ));


        static GenerationResult<UnionTypeSchema> Error(Diagnostic diagnostic) => GenerationResult<UnionTypeSchema>.Empty.AddDiagnostics(diagnostic);
    }

    private static (string parameterName, string methodName) DeriveParameterAndStaticMethodName(string typeName,
        string baseTypeName)
    {
        var candidates = ImmutableList<string>.Empty;
        candidates = AddIfDiffersAndValid(typeName.TrimBaseTypeName(baseTypeName));
        candidates = AddIfDiffersAndValid(typeName.Trim('_'));
        candidates = candidates.Add(typeName);

        var parameterName = candidates[0].FirstToLower().PrefixAtIfKeyword();
        var methodName = candidates[0].FirstToUpper().PrefixAtIfKeyword();

        return (parameterName, methodName);

        ImmutableList<string> AddIfDiffersAndValid(string candidate) =>
            DiffersAndValid(typeName, candidate)
                ? candidates.Add(candidate)
                : candidates;
    }

    static bool DiffersAndValid(string typeName, string candidate) =>
        candidate != typeName
        && !string.IsNullOrEmpty(candidate)
        && char.IsLetter(candidate[0]);

    static StaticFactoryMethodsInfo BuildFactoryInfo(BaseTypeDeclarationSyntax unionTypeClass, Compilation compilation)
    {
        var staticMethods = unionTypeClass.ChildNodes()
            .OfType<MethodDeclarationSyntax>()
            .Where(m => m.Modifiers.HasModifier(SyntaxKind.StaticKeyword))
            .Select(m => m.ToMemberInfo(m.Name(), compilation))
            .ToImmutableArray();

        var staticFields = unionTypeClass.ChildNodes()
            .SelectMany(s => s switch
            {
                FieldDeclarationSyntax f when f.Modifiers.HasModifier(SyntaxKind.StaticKeyword) => f.Declaration
                    .Variables.Select(v => v.Identifier.Text),
                PropertyDeclarationSyntax p when p.Modifiers.HasModifier(SyntaxKind.StaticKeyword) => new[]
                {
                    p.Name()
                },
                _ => Array.Empty<string>()
            })
            .ToImmutableArray();

        return new(staticMethods, staticFields);
    }

    static GenerationResult<ImmutableArray<DerivedType>> ToOrderedCases(CaseOrder caseOrder,
        IEnumerable<(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax node, int? caseIndex, int
            numberOfConctreteBaseTypes)> derivedTypes, Compilation compilation, bool getConstructors, string baseTypeName)
    {
        var ordered = derivedTypes.OrderByDescending(d => d.numberOfConctreteBaseTypes);
        ordered = caseOrder switch
        {
            CaseOrder.Alphabetic => ordered.ThenBy(d => d.node.QualifiedNameWithGenerics().Name),
            CaseOrder.AsDeclared => ordered.ThenBy(d => d.node.SyntaxTree.FilePath)
                .ThenBy(d => d.node.Span.Start),
            CaseOrder.Explicit => ordered.ThenBy(d => d.caseIndex),
            _ => throw new ArgumentOutOfRangeException(nameof(caseOrder), caseOrder, null)
        };

        var result = ordered.ToImmutableArray();

        var errors = ImmutableArray<DiagnosticInfo>.Empty;

        switch (caseOrder)
        {
            case CaseOrder.Alphabetic:
            case CaseOrder.AsDeclared:
                foreach (var t in result.Where(r => r.caseIndex != null))
                {
                    var message = $"Explicit case index on {t.node.Name()} is ignored, because CaseOrder on UnionTypeAttribute is {caseOrder}. Set it CaseOrder.Explicit for explicit ordering.";
                    var diagnostic = Diagnostics.MisleadingCaseOrdering(message, t.node.GetLocation());
                    errors = errors.Add(diagnostic);
                }
                break;
            case CaseOrder.Explicit:
                foreach (var t in result.Where(r => r.caseIndex == null))
                {
                    var message = $"Missing case index on {t.node.Name()}. Please add UnionCaseAttribute for explicit case ordering.";
                    errors = errors.Add(Diagnostics.CaseIndexNotSet(message, t.node.GetLocation()));
                }

                foreach (var group in result.Where(r => r.caseIndex != null)
                             .GroupBy(r => r.caseIndex)
                             .Where(g => g.Count() > 1))
                {
                    var message = $"Cases {group.Select(g => g.node.Name()).ToSeparatedString()} define the same case index. Order is not guaranteed.";
                    errors = errors.Add(Diagnostics.AmbiguousCaseIndex(message, group.First().node.GetLocation()));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(caseOrder), caseOrder, null);
        }

        var derived = result.Select(d =>
        {
            var qualifiedTypeName = d.node.QualifiedNameWithGenerics();
            var fullNamespace = d.symbol.GetFullNamespace();
            var constructors = ImmutableArray<CallableMemberInfo>.Empty;
            var requiredMembers = ImmutableArray<PropertyOrFieldInfo>.Empty;

            if (getConstructors)
            {
                constructors = constructors.AddRange(d.node
                    .ChildNodes()
                    .OfType<ConstructorDeclarationSyntax>()
                    .Select(c => c.ToMemberInfo(c.Identifier.Text, compilation)));

                //primary constructors
                if (d.node is TypeDeclarationSyntax { ParameterList: not null } typeDeclaration)
                    constructors = constructors.Add(
                        new(
                            Name: d.node.Name(),
                            Modifiers: d.node.Modifiers.ToEquatableModifiers(),
                            Parameters: typeDeclaration.ParameterList.Parameters
                                .Select(p => p.ToParameterInfo(compilation))
                                .ToImmutableArray())
                    );

                requiredMembers = requiredMembers.AddRange(d.node.ChildNodes()
                    .OfType<MemberDeclarationSyntax>()
                    .Where(m => m.Modifiers.HasRequiredModifier())
                    .Select(m => m.ToPropertyOrFieldInfo(compilation)));
            }

            var (parameterName, staticMethodName) =
                DeriveParameterAndStaticMethodName(qualifiedTypeName.Name, baseTypeName);

            return new DerivedType(
                fullTypeName: $"{(fullNamespace != null ? $"{fullNamespace}." : "")}{qualifiedTypeName}",
                constructors: constructors,
                requiredMembers: requiredMembers,
                parameterName: parameterName,
                staticFactoryMethodName: staticMethodName);
        }).ToImmutableArray();

        return new(derived, errors, true);
    }
}

enum CaseOrder
{
    Alphabetic,
    AsDeclared,
    Explicit
}

class FindConcreteDerivedTypesWalker : CSharpSyntaxWalker
{
    readonly List<(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax node, int? caseIndex)> m_DerivedClasses = new();
    readonly SemanticModel m_SemanticModel;
    readonly ITypeSymbol m_BaseClass;

    FindConcreteDerivedTypesWalker(SemanticModel semanticModel, ITypeSymbol baseClass)
    {
        m_SemanticModel = semanticModel;
        m_BaseClass = baseClass;
    }

    public static IEnumerable<(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax node, int? caseIndex, int numberOfConctreteBaseTypes)> Get(SyntaxNode node, ITypeSymbol baseClass, SemanticModel semanticModel)
    {
        var me = new FindConcreteDerivedTypesWalker(semanticModel, baseClass);
        me.Visit(node);
        return me.m_DerivedClasses.Select(d => (d.symbol, d.node, d.caseIndex, numberOfConctreteBaseTypes: me.m_DerivedClasses.Count(t => d.symbol.InheritsFrom(t.symbol))));
    }

    public override void VisitClassDeclaration(ClassDeclarationSyntax node)
    {
        CheckIsConcreteDerived(node);
        base.VisitClassDeclaration(node);
    }

    public override void VisitRecordDeclaration(RecordDeclarationSyntax node)
    {
        CheckIsConcreteDerived(node);
        base.VisitRecordDeclaration(node);
    }

    public override void VisitStructDeclaration(StructDeclarationSyntax node)
    {
        CheckIsConcreteDerived(node);
        base.VisitStructDeclaration(node);
    }

    void CheckIsConcreteDerived(BaseTypeDeclarationSyntax node)
    {
        var isAbstract = node.Modifiers.Any(m => m.Text.ToString() == "abstract");
        if (!isAbstract)
        {
            var symbol = m_SemanticModel.GetDeclaredSymbol(node);
            if (symbol != null && (symbol.InheritsFrom(m_BaseClass) || symbol.Implements(m_BaseClass)))
            {
                var attribute = node.AttributeLists
                    .SelectMany(l => l.Attributes)
                    .FirstOrDefault(a => a.GetAttributeFullName(m_SemanticModel) == UnionTypeGenerator.UnionCaseAttribute);

                var caseIndex = TryGetCaseIndex(attribute);

                m_DerivedClasses.Add((symbol, node, caseIndex));
            }
        }
    }

    static int? TryGetCaseIndex(AttributeSyntax? attribute)
    {
        if (attribute == null)
            return null;

        var expressionSyntax = attribute.ArgumentList?.Arguments[0].Expression;
        if (expressionSyntax is not LiteralExpressionSyntax l)
        {
            //reportDiagnostics(Diagnostics.InvalidAttributeUsage("Expected literal numeric value as argument for UnionCaseAttribute", attribute.GetLocation()));
            return null;
        }
        return l.Token.Value as int?;
    }
}