using FunicularSwitch.Generators.Common;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Monad;

internal class Parser
{
    public static GenerationResult<ExtendMonadInfo> GetExtendedMonadSchema(INamedTypeSymbol targetSymbol, ExtendMonadAttribute extendMonadAttribute, CancellationToken cancellationToken)
    {
        var accessModifier = Transformer.Parser.DetermineAccessModifier(targetSymbol);
        return
            from monadInfo in Transformer.Parser.ResolveMonadDataFromMonadType(targetSymbol, cancellationToken)
            let staticMonadGenerationInfo = Transformer.Parser.BuildStaticMonad(
                targetSymbol.Name,
                monadInfo.GenericTypeName,
                accessModifier,
                [],
                monadInfo,
                monadInfo,
                monadInfo,
                false)
            select new ExtendMonadInfo(
                targetSymbol.FullTypeNameWithNamespace(),
                targetSymbol.GetFullNamespace(),
                staticMonadGenerationInfo);
    }
}
