using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using FunicularSwitch.Generators.Parsing;
using FunicularSwitch.Generators.Transformer;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.ExtendMonad;

internal class Parser
{
    public static GenerationResult<ExtendMonadInfo> GetExtendedMonadSchema(INamedTypeSymbol targetSymbol, ExtendMonadAttribute extendMonadAttribute, CancellationToken cancellationToken) =>
        from monadInfo in MonadParser.ResolveMonadDataFromMonadType(targetSymbol, cancellationToken)
        let staticMonadGenerationInfo = new StaticMonadGenerationInfo(
            targetSymbol.Name,
            targetSymbol.GetActualAccessibility(),
            [],
            MonadMethods.CreateExtendMonadMethods(
                monadInfo.GenericTypeName,
                monadInfo,
                [
                    MonadMethods.Create(
                        monadInfo.ExtraArity,
                        string.Empty,
                        _ => string.Empty,
                        ["A"],
                        _ =>
                        [
                            new ParameterGenerationInfo("A", string.Empty),
                        ],
                        monadInfo.ReturnMethod.Name,
                        _ => string.Empty
                    ),
                    MonadMethods.Create(
                        monadInfo.ExtraArity,
                        string.Empty,
                        _ => string.Empty,
                        ["A", "B"],
                        t =>
                        [
                            new ParameterGenerationInfo(monadInfo.GenericTypeName([..t, "A"]), string.Empty),
                            new ParameterGenerationInfo(Types.Func("A", monadInfo.GenericTypeName([..t, "B"])), string.Empty),
                        ],
                        monadInfo.BindMethod.Name,
                        _ => string.Empty
                    ),
                ]
            )
        )
        select new ExtendMonadInfo(
            targetSymbol.FullTypeNameWithNamespace(),
            targetSymbol.GetFullNamespace(),
            staticMonadGenerationInfo);
}
