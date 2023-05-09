using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.EnumType;

static class Parser
{
    public static IEnumerable<EnumTypeSchema> GetEnumTypes(Compilation compilation,
        ImmutableArray<BaseTypeDeclarationSyntax> enumTypeClasses, Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken) =>
        enumTypeClasses
	        .Select(enumTypeClass =>
        {
            var semanticModel = compilation.GetSemanticModel(enumTypeClass.SyntaxTree);
            var enumTypeSymbol = semanticModel.GetDeclaredSymbol(enumTypeClass);

            if (enumTypeSymbol == null) //TODO: report diagnostics
	            return null;

            var fullTypeName = enumTypeSymbol.FullTypeNameWithNamespace();
            var acc = enumTypeSymbol.DeclaredAccessibility;
            if (acc is Accessibility.Private or Accessibility.Protected)
            {
	            reportDiagnostic(Diagnostics.EnumTypeIsNotAccessible($"{fullTypeName} needs at least internal accessibility", enumTypeClass.GetLocation()));
	            return null;
            }
            
            var attribute = enumTypeClass.AttributeLists
                .Select(l => l.Attributes.First(a => a.GetAttributeFullName(semanticModel) == EnumTypeGenerator.EnumTypeAttribute))
                .First();

            var enumCaseOrder = TryGetCaseOrder(attribute, reportDiagnostic);
            var fullNamespace = enumTypeSymbol.GetFullNamespace();
            if (enumTypeClass is EnumDeclarationSyntax enumDeclarationSyntax)
            {
                var fullTypeNameWithNamespace = enumTypeSymbol.FullTypeNameWithNamespace();
                var enumCases = enumDeclarationSyntax.Members
                    .Select(m => new DerivedType( $"{fullTypeNameWithNamespace}.{m.Identifier.Text}", m.Identifier.Text));
                if (enumCaseOrder == EnumCaseOrder.Alphabetic)
                {
                    enumCases = enumCases.OrderBy(m => m.FullTypeName);
                }
                
                return new EnumTypeSchema(
                    Namespace: fullNamespace,
                    TypeName: enumTypeSymbol.Name,
                    FullTypeName: fullTypeName,
                    Cases:  enumCases
                        .ToImmutableArray(),
                    acc is Accessibility.NotApplicable or Accessibility.Internal
                );
            }

            var derivedTypes = compilation.SyntaxTrees.SelectMany(t =>
            {
                var root = t.GetRoot(cancellationToken);
                var treeSemanticModel = t != enumTypeClass.SyntaxTree ? compilation.GetSemanticModel(t) : semanticModel;
                return FindConcreteDerivedTypesWalker.Get(root, enumTypeSymbol, treeSemanticModel);
            });

            return new EnumTypeSchema(
                Namespace: fullNamespace,
                TypeName: enumTypeSymbol.Name,
                FullTypeName: fullTypeName,
                Cases: ToOrderedCases(enumCaseOrder, derivedTypes, reportDiagnostic)
                    .ToImmutableArray(),
                acc is Accessibility.NotApplicable or Accessibility.Internal
            );
        })
	        .Where(enumTypeClass => enumTypeClass != null)!;
    
    static EnumCaseOrder TryGetCaseOrder(AttributeSyntax attribute, Action<Diagnostic> reportDiagnostics)
    {
        if ((attribute.ArgumentList?.Arguments.Count ?? 0) < 1)
            return EnumCaseOrder.AsDeclared;

        var expressionSyntax = attribute.ArgumentList?.Arguments[0].Expression;
        if (expressionSyntax is not MemberAccessExpressionSyntax l)
        {
            //TODO: report diagnostics
            return EnumCaseOrder.AsDeclared;
        }
        
        return (EnumCaseOrder)Enum.Parse(typeof(EnumCaseOrder), l.Name.ToString());
    }

    static IEnumerable<DerivedType> ToOrderedCases(EnumCaseOrder enumCaseOrder, IEnumerable<(INamedTypeSymbol symbol, BaseTypeDeclarationSyntax node, int? caseIndex, int numberOfConctreteBaseTypes)> derivedTypes, Action<Diagnostic> reportDiagnostic)
    {
        var ordered = derivedTypes.OrderByDescending(d => d.numberOfConctreteBaseTypes);
        ordered = enumCaseOrder switch
        {
            EnumCaseOrder.Alphabetic => ordered.ThenBy(d => d.node.QualifiedName().Name),
            EnumCaseOrder.AsDeclared => ordered.ThenBy(d => d.node.SyntaxTree.FilePath)
                .ThenBy(d => d.node.Span.Start),
            _ => throw new ArgumentOutOfRangeException(nameof(enumCaseOrder), enumCaseOrder, null)
        };

        var result = ordered.ToImmutableArray();

        switch (enumCaseOrder)
        {
            case EnumCaseOrder.Alphabetic:
            case EnumCaseOrder.AsDeclared:
                foreach (var t in result.Where(r => r.caseIndex != null))
                {
                    var message = $"Explicit case index on {t.node.Name()} is ignored, because CaseOrder on EnumTypeAttribute is {enumCaseOrder}. Set it CaseOrder.Explicit for explicit ordering.";
                    reportDiagnostic(Diagnostics.MisleadingCaseOrdering(message, t.node.GetLocation()));
                }
                break;
            default:
                throw new ArgumentOutOfRangeException(nameof(enumCaseOrder), enumCaseOrder, null);
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

enum EnumCaseOrder
{
    Alphabetic,
    AsDeclared
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
                    .Select(l => l.Attributes.First(a => a.GetAttributeFullName(m_SemanticModel) == EnumTypeGenerator.EnumCaseAttribute))
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
            //reportDiagnostics(Diagnostics.InvalidAttributeUsage("Expected literal numeric value as argument for EnumCaseAttribute", attribute.GetLocation()));
            return null;
        }
        return l.Token.Value as int?;
    }
}