using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using FunicularSwitch.Generators.Transformer;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.ExtendMonad;

internal class Parser
{
    public static GenerationResult<ExtendMonadInfo> GetExtendedMonadSchema(INamedTypeSymbol targetSymbol, ExtendMonadAttribute extendMonadAttribute, CancellationToken cancellationToken)
    {
        var accessModifier = Transformer.Parser.DetermineAccessModifier(targetSymbol);
        return
            from monadInfo in Transformer.Parser.ResolveMonadDataFromMonadType(targetSymbol, cancellationToken)
            let staticMonadGenerationInfo = new StaticMonadGenerationInfo(
                targetSymbol.Name,
                accessModifier,
                [],
                MonadMethods.CreateExtendMonadMethods(
                    monadInfo.GenericTypeName,
                    monadInfo
                )
            )
            select new ExtendMonadInfo(
                targetSymbol.FullTypeNameWithNamespace(),
                targetSymbol.GetFullNamespace(),
                staticMonadGenerationInfo);
    }
}
