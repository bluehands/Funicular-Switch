using DarkLink.RoslynHelpers;
using FunicularSwitch.Generators.Generation;

namespace FunicularSwitch.Generators.ExtendMonad;

[GenerateAttribute(AttributeTargets.Class | AttributeTargets.Struct, Namespace = "FunicularSwitch.Generators", Inherited = false, Docs = Constants.ExperimentalGeneratorDocs)]
public partial record ExtendMonadAttribute;
