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
            .OfType<EnumDeclarationSyntax>()
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
            var fullTypeNameWithNamespace = enumTypeSymbol.FullTypeNameWithNamespace();
            var enumCases = enumTypeClass.Members
                .Select(m => new DerivedType($"{fullTypeNameWithNamespace}.{m.Identifier.Text}", m.Identifier.Text));

            return new EnumTypeSchema(
                Namespace: enumTypeSymbol.GetFullNamespace(),
                TypeName: enumTypeSymbol.Name,
                FullTypeName: fullTypeName,
                Cases: (enumCaseOrder == EnumCaseOrder.AsDeclared 
                    ? enumCases 
                    : enumCases.OrderBy(m => m.FullTypeName))
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
}

enum EnumCaseOrder
{
    Alphabetic,
    AsDeclared
}