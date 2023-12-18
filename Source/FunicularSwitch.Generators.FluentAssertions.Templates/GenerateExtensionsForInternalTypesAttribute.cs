using System;

namespace FunicularSwitch.Generators.FluentAssertions.Templates;

[AttributeUsage(validOn: AttributeTargets.Assembly, AllowMultiple = true)]
public class GenerateExtensionsForInternalTypesAttribute : Attribute
{
    public Type AssemblySpecifier { get; }

    public GenerateExtensionsForInternalTypesAttribute(Type assemblySpecifier)
    {
        this.AssemblySpecifier = assemblySpecifier;
    }
}