using System;
using DarkLink.RoslynHelpers;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

[GenerateAttribute(AttributeTargets.Class | AttributeTargets.Struct, Namespace = "FunicularSwitch.Generators", Inherited = false)]
public partial record TransformMonadAttribute(INamedTypeSymbol MonadType, INamedTypeSymbol TransformerType);
