using System.Collections.Immutable;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

namespace FunicularSwitch.Generators.UnionType;

static class Parser
{
    public static IEnumerable<UnionTypeSchema> GetUnionTypes(Compilation compilation,
        ImmutableArray<BaseTypeDeclarationSyntax> unionTypeClasses, Action<Diagnostic> reportDiagnostic,
        CancellationToken cancellationToken) =>
        unionTypeClasses.Select(unionTypeClass =>
        {
            var semanticModel = compilation.GetSemanticModel(unionTypeClass.SyntaxTree);
            var unionTypeSymbol = semanticModel.GetDeclaredSymbol(unionTypeClass);
            if (unionTypeSymbol == null)
                throw new ArgumentException("Cannot get union type symbol"); //TODO: report diagnostics

            var derivedTypes = compilation.SyntaxTrees.SelectMany(t =>
            {
                var root = t.GetRoot(cancellationToken);
                var treeSemanticModel =
                    t != unionTypeClass.SyntaxTree ? compilation.GetSemanticModel(t) : semanticModel;
                return FindConcreteDerivedTypesWalker.Get(root, unionTypeSymbol, treeSemanticModel);
            });

            return new UnionTypeSchema(
                unionTypeSymbol.GetFullNamespace(),
                unionTypeSymbol.Name,
                derivedTypes.Select(d => new DerivedType(d.@namespace + "." + d.typeName, d.typeName.Name))
                    .ToImmutableArray()
            );

        });
}

class FindConcreteDerivedTypesWalker : CSharpSyntaxWalker
{
    readonly List<(string @namespace, QualifiedTypeName typeName)> m_DerivedClasses = new();
    readonly SemanticModel m_SemanticModel;
    readonly ITypeSymbol m_BaseClass;

    FindConcreteDerivedTypesWalker(SemanticModel semanticModel, ITypeSymbol baseClass)
    {
        m_SemanticModel = semanticModel;
        m_BaseClass = baseClass;
    }

    public static List<(string @namespace, QualifiedTypeName typeName)> Get(SyntaxNode node, ITypeSymbol baseClass, SemanticModel semanticModel)
    {
        var me = new FindConcreteDerivedTypesWalker(semanticModel, baseClass);
        me.Visit(node);
        return me.m_DerivedClasses;
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
            if (symbol != null && symbol.InheritsFrom(m_BaseClass))
            {
                m_DerivedClasses.Add((symbol.GetFullNamespace(), node.QualifiedName()));
            }
        }
    }
}