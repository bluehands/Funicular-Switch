using System;
using DarkLink.RoslynHelpers;
using FunicularSwitch.Generators.Generation;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

[GenerateAttribute(AttributeTargets.Class | AttributeTargets.Struct, Namespace = "FunicularSwitch.Generators", Inherited = false, Docs = Constants.ExperimentalGeneratorDocs)]
public partial record TransformMonadAttribute(INamedTypeSymbol MonadType, INamedTypeSymbol TransformerType, params INamedTypeSymbol[] ExtraTransformerTypes);