using System.Collections.Immutable;
using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.ResultType;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.UnionType;

static class Parser
{
    public static IEnumerable<UnionTypeSchema> GetUnionTypes(Compilation compilation,
        ImmutableArray<BaseTypeDeclarationSyntax> unionTypeClasses, Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken) =>
        unionTypeClasses
	        .Select(unionTypeClass =>
        {
            var semanticModel = compilation.GetSemanticModel(unionTypeClass.SyntaxTree);
            var unionTypeSymbol = semanticModel.GetDeclaredSymbol(unionTypeClass);

            if (unionTypeSymbol == null) //TODO: report diagnostics
	            return null;

            var fullTypeName = unionTypeSymbol.FullTypeNameWithNamespace();
            var acc = unionTypeSymbol.DeclaredAccessibility;
            if (acc is Accessibility.Private or Accessibility.Protected)
            {
	            reportDiagnostic(Diagnostics.UnionTypeIsNotAccessible($"{fullTypeName} needs at least internal accessibility", unionTypeClass.GetLocation()));
	            return null;
            }
            
            var attribute = unionTypeClass.AttributeLists
                .Select(l => l.Attributes.First(a => a.GetAttributeFullName(semanticModel) == UnionTypeGenerator.UnionTypeAttribute))
                .First();

            var caseOrder = TryGetCaseOrder(attribute, reportDiagnostic);

            var fullNamespace = unionTypeSymbol.GetFullNamespace();
            if (unionTypeClass is EnumDeclarationSyntax enumDeclarationSyntax)
            {
                var fullTypeNameWithNamespace = unionTypeSymbol.FullTypeNameWithNamespace();
                var unionCases = enumDeclarationSyntax.Members
                    .Select(m => new DerivedType( $"{fullTypeNameWithNamespace}.{m.Identifier.Text}", m.Identifier.Text));
                if (caseOrder == CaseOrder.Alphabetic)
                {
                    unionCases = unionCases.OrderBy(m => m.FullTypeName);
                }
                
                return new UnionTypeSchema(
                    Namespace: fullNamespace,
                    TypeName: unionTypeSymbol.FullTypeName(),
                    FullTypeName: fullTypeName,
                    Cases:  unionCases
                        .ToImmutableArray(),
                    acc is Accessibility.NotApplicable or Accessibility.Internal,
                    true
                );
            }

            var derivedTypes = compilation.SyntaxTrees.SelectMany(t =>
            {
                var root = t.GetRoot(cancellationToken);
                var treeSemanticModel = t != unionTypeClass.SyntaxTree ? compilation.GetSemanticModel(t) : semanticModel;
                
                return FindConcreteDerivedTypesWalker.Get(root, unionTypeSymbol, treeSemanticModel);
            });

            return new UnionTypeSchema(
                Namespace: fullNamespace,
                TypeName: unionTypeSymbol.Name,
                FullTypeName: fullTypeName,
                Cases: ToOrderedCases(caseOrder, derivedTypes, reportDiagnostic)
                    .ToImmutableArray(),
                acc is Accessibility.NotApplicable or Accessibility.Internal,
                false
            );

        })
	        .Where(unionTypeClass => unionTypeClass != null)!;

    static CaseOrder TryGetCaseOrder(AttributeSyntax attribute, Action<Diagnostic> reportDiagnostics)
    {
        if ((attribute.ArgumentList?.Arguments.Count ?? 0) < 1)
            return CaseOrder.Alphabetic;

        var expressionSyntax = attribute.ArgumentList?.Arguments[0].Expression;
        if (expressionSyntax is not MemberAccessExpressionSyntax l)
        {
            //TODO: report diagnostics
            return CaseOrder.Alphabetic;
        }
        
        return (CaseOrder)Enum.Parse(typeof(CaseOrder), l.Name.ToString());
    }

    static IEnumerable<DerivedType> ToOrderedCases(CaseOrder caseOrder, IEnumerable<(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax node, int? caseIndex, int numberOfConctreteBaseTypes)> derivedTypes, Action<Diagnostic> reportDiagnostic)
    {
        var ordered = derivedTypes.OrderByDescending(d => d.numberOfConctreteBaseTypes);
        ordered = caseOrder switch
        {
            CaseOrder.Alphabetic => ordered.ThenBy(d => d.node.QualifiedName().Name),
            CaseOrder.AsDeclared => ordered.ThenBy(d => d.node.SyntaxTree.FilePath)
                .ThenBy(d => d.node.Span.Start),
            CaseOrder.Explicit => ordered.ThenBy(d => d.caseIndex),
            _ => throw new ArgumentOutOfRangeException(nameof(caseOrder), caseOrder, null)
        };

        var result = ordered.ToImmutableArray();

        switch (caseOrder)
        {
            case CaseOrder.Alphabetic:
            case CaseOrder.AsDeclared:
                foreach (var t in result.Where(r => r.caseIndex != null))
                {
                    var message = $"Explicit case index on {t.node.Name()} is ignored, because CaseOrder on UnionTypeAttribute is {caseOrder}. Set it CaseOrder.Explicit for explicit ordering.";
                    reportDiagnostic(Diagnostics.MisleadingCaseOrdering(message, t.node.GetLocation()));
                }
                break;
            case CaseOrder.Explicit:
                foreach (var t in result.Where(r => r.caseIndex == null))
                {
                    var message = $"Missing case index on {t.node.Name()}. Please add UnionCaseAttribute for explicit case ordering.";
                    reportDiagnostic(Diagnostics.CaseIndexNotSet(message, t.node.GetLocation()));
                }

                foreach (var group in result.Where(r => r.caseIndex != null)
                             .GroupBy(r => r.caseIndex)
                             .Where(g => g.Count() > 1))
                {
                    var message = $"Cases {group.Select(g => g.node.Name()).ToSeparatedString()} define the same case index. Order is not guaranteed.";
                    reportDiagnostic(Diagnostics.AmbiguousCaseIndex(message, group.First().node.GetLocation()));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(caseOrder), caseOrder, null);
        }

        return result.Select(d =>
        {
            var qualifiedTypeName = d.node.QualifiedName();
            var fullNamespace = d.symbol.GetFullNamespace();
            return new DerivedType(
                fullTypeName: $"{(fullNamespace != null ? $"{fullNamespace}." : "")}{qualifiedTypeName}",
                typeName: qualifiedTypeName.Name);
        });
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

    void CheckIsConcreteDerived(BaseTypeDeclarationSyntax node)
    {
        var isAbstract = node.Modifiers.Any(m => m.Text.ToString() == "abstract");
        if (!isAbstract)
        {
            var symbol = m_SemanticModel.GetDeclaredSymbol(node);
            if (symbol != null && (symbol.InheritsFrom(m_BaseClass) || symbol.Implements(m_BaseClass)))
            {
                var attribute = node.AttributeLists
                    .Select(l => l.Attributes.First(a => a.GetAttributeFullName(m_SemanticModel) == UnionTypeGenerator.UnionCaseAttribute))
                    .FirstOrDefault();
                
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