using System;
using DarkLink.RoslynHelpers;
using Microsoft.CodeAnalysis;

namespace FunicularSwitch.Generators.Transformer;

[GenerateAttribute(AttributeTargets.Class, Namespace = "FunicularSwitch.Transformers", Inherited = false)]
public partial record MonadTransformerAttribute(INamedTypeSymbol MonadType);
