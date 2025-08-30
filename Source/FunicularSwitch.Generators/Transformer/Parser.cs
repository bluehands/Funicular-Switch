using FunicularSwitch.Generators.Common;
using FunicularSwitch.Generators.Generation;
using FunicularSwitch.Generators.Parsing;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

internal static class Parser
{
    public static GenerationResult<TransformMonadInfo> GetTransformedMonadSchema(
        INamedTypeSymbol transformedMonadSymbol,
        TransformMonadAttribute transformMonadAttribute,
        CancellationToken cancellationToken)
    {
        cancellationToken.ThrowIfCancellationRequested();
        var outerMonadType = transformMonadAttribute.MonadType;

        return
            from outerMonadData in MonadParser.ResolveMonadDataFromMonadType(outerMonadType, cancellationToken)
            let transformerTypes = new[] {transformMonadAttribute.TransformerType}
                .Concat(transformMonadAttribute.ExtraTransformerTypes)
                .ToList()
            from chainedMonads in transformerTypes
                .Aggregate(
                    (GenerationResult<IReadOnlyList<MonadInfo>>) new[] {outerMonadData},
                    (acc, cur) =>
                        acc.Bind(acc_ =>
                            TransformMonad(acc_.Last(), cur, cancellationToken).Map<IReadOnlyList<MonadInfo>>(transformMonad =>
                                [..acc_, transformMonad])))
            let chainedMonad = chainedMonads.Last()
            let implementations = chainedMonads
                .Take(chainedMonads.Count - 1)
                .Where(x => !x.ImplementsMonadInterface)
                .Select(GenerateImplementationForMonad)
                .ToList()
            let transformMonadData = new TransformMonadInfo(
                transformedMonadSymbol.GetFullNamespace()!,
                transformedMonadSymbol.FullTypeNameWithNamespace(),
                transformedMonadSymbol.IsStatic ? chainedMonad.GenericTypeName : FullGenericType,
                chainedMonad,
                transformedMonadSymbol.IsStatic
                    ? null
                    : BuildGenericMonad(
                        transformedMonadSymbol,
                        chainedMonad
                    ),
                BuildStaticMonad(
                    transformedMonadSymbol.Name,
                    transformedMonadSymbol.IsStatic ? chainedMonad.GenericTypeName : FullGenericType,
                    transformedMonadSymbol.GetActualAccessibility(),
                    implementations,
                    chainedMonad,
                    outerMonadData,
                    outerMonadData // TODO: determine actual inner monad
                ))
            select transformMonadData;

        string FullGenericType(IReadOnlyList<string> t) => $"global::{transformedMonadSymbol.FullTypeNameWithNamespace()}<{string.Join(", ", t)}>";

        static ConstructType ChainGenericTypeName(ConstructType outer, ConstructType inner) =>
            x => outer([inner(x)]);

        static MethodInfo CombineReturn(MethodInfo outer, MonadInfo inner) =>
            new MethodInfo(
                DetermineMethodName(outer.Name, inner.ReturnMethod.Name, "Return"),
                (t, p) => $"{outer.Invoke([inner.GenericTypeName([t[0]])], [inner.ReturnMethod.Invoke(t, p)])}");

        static MethodInfo TransformBind(MonadInfo outer, MonadInfo inner, string transformerTypeName, ConstructType outerInterfaceImplName)
        {
            var chainedGenericType = ChainGenericTypeName(outer.GenericTypeName, inner.GenericTypeName);

            return new MethodInfo(
                DetermineMethodName(outer.BindMethod.Name, inner.BindMethod.Name, "Bind"),
                (t, p) =>
                {
                    var ma = $"({outerInterfaceImplName([inner.GenericTypeName([t[0]])])}){p[0]}";
                    var fn = $"[{Constants.DebuggerStepThroughAttribute}](a) => ({outerInterfaceImplName([inner.GenericTypeName([t[1]])])})(new global::System.Func<{t[0]}, {chainedGenericType([t[1]])}>({p[1]}).Invoke(a))"; // A -> Monad<X<B>>

                    var call = $"{transformerTypeName}.BindT<{t[0]}, {t[1]}>({ma}, {fn}).Cast<{chainedGenericType([t[1]])}>()";
                    return call;
                });
        }

        static GenerationResult<MonadInfo> TransformMonad(MonadInfo outer, INamedTypeSymbol transformerType,
            CancellationToken cancellationToken1 = default) =>
            ResolveMonadDataFromTransformerType(transformerType, cancellationToken1)
                .Map(innerMonad =>
                {
                    var transformerTypeName = $"global::{transformerType.FullTypeNameWithNamespace()}";
                    var outerInterfaceImplementation =
                        !outer.ImplementsMonadInterface ? GenerateImplementationForMonad(outer) : null;
                    var outerInterfaceName =
                        outerInterfaceImplementation?.GenericTypeName ?? outer.GenericTypeName;

                    var transformedMonad = new MonadInfo(
                        ChainGenericTypeName(outer.GenericTypeName, innerMonad.GenericTypeName),
                        CombineReturn(outer.ReturnMethod, innerMonad),
                        TransformBind(outer, innerMonad, transformerTypeName, outerInterfaceName));
                    return transformedMonad;
                });
    }

    private static GenericMonadGenerationInfo BuildGenericMonad(
        INamedTypeSymbol transformedMonadSymbol,
        MonadInfo chainedMonad)
    {
        var typeModifier = DetermineTypeModifier(transformedMonadSymbol);
        var isRecord = transformedMonadSymbol.IsRecord;
        var typeParameter = transformedMonadSymbol.TypeArguments[0].Name;

        return new(
            transformedMonadSymbol.GetActualAccessibility(),
            typeModifier,
            transformedMonadSymbol.Name,
            typeParameter,
            $"{transformedMonadSymbol.Name}<{typeParameter}>",
            isRecord,
            chainedMonad
        );
    }

    private static StaticMonadGenerationInfo BuildStaticMonad(
        string typeName,
        ConstructType genericTypeName,
        Accessibility accessibility,
        IReadOnlyList<MonadImplementationGenerationInfo> monadImplementations,
        MonadInfo chainedMonad,
        MonadInfo outerMonad,
        MonadInfo innerMonad,
        bool generateCoreMethods = true)
    {
        var coreMethods = generateCoreMethods
            ? MonadMethods.CreateCoreMonadMethods(genericTypeName,
                chainedMonad,
                outerMonad,
                innerMonad
            )
            : [];

        return new StaticMonadGenerationInfo(
            typeName,
            accessibility,
            monadImplementations,
            [
                ..coreMethods,
                ..MonadMethods.CreateExtendMonadMethods(
                    genericTypeName,
                    chainedMonad,
                    coreMethods
                ),
            ]
        );
    }

    private static string DetermineMethodName(string outerName, string innerName, string defaultName)
    {
        if (outerName == innerName)
            return outerName;

        if (outerName == defaultName)
            return innerName;

        if (innerName == defaultName)
            return outerName;

        return $"{outerName}{innerName}";
    }

    private static string DetermineTypeModifier(INamedTypeSymbol type)
    {
        var modifiers = new List<string>();

        if (type.IsReadOnly)
            modifiers.Add("readonly");

        modifiers.Add("partial");

        if (type.IsRecord)
            modifiers.Add("record");

        if (!type.IsReferenceType)
            modifiers.Add("struct");
        else if (!type.IsRecord)
            modifiers.Add("class");

        return string.Join(" ", modifiers);
    }

    private static MonadImplementationGenerationInfo GenerateImplementationForMonad(MonadInfo info)
    {
        var baseName = info.GenericTypeName(["_"])
            .Replace('.', '_')
            .Replace('<', '_')
            .Replace('>', '_')
            .Replace(':', '_')
            .TrimEnd('_')
            [8..];
        return new MonadImplementationGenerationInfo(
            t => $"Impl__{baseName}<{string.Join(", ", t)}>",
            info);
    }

    private static GenerationResult<MonadInfo> ResolveMonadDataFromTransformerType(INamedTypeSymbol transformerType,
        CancellationToken cancellationToken)
    {
        var attributeData = transformerType.GetAttributes()
            .FirstOrDefault(x =>
                x.AttributeClass?.FullTypeNameWithNamespace() == MonadTransformerAttribute.ATTRIBUTE_NAME);

        if (attributeData is null)
            return new DiagnosticInfo(Diagnostics.MonadTransformerNoAttribute(transformerType));

        var hasBindTMethod = transformerType
            .GetMembers()
            .OfType<IMethodSymbol>()
            .Any(IsBindTMethod);

        if (!hasBindTMethod)
            return new DiagnosticInfo(Diagnostics.MissingBindTMethod(transformerType));

        var monadTransformerAttribute = MonadTransformerAttribute.From(attributeData);
        var staticMonadType = monadTransformerAttribute.MonadType;
        var monadData = MonadParser.ResolveMonadDataFromMonadType(staticMonadType, cancellationToken);
        return monadData;

        static bool IsBindTMethod(IMethodSymbol method)
        {
            if (method.Name != "BindT") return false;
            return true;
        }
    }
}
