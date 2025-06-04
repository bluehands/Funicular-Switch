#nullable enable
using System;

namespace FunicularSwitch.Generators.AwesomeAssertions.Templates;

[AttributeUsage(validOn: AttributeTargets.Assembly, AllowMultiple = true)]
public class GenerateExtensionsForInternalTypesAttribute : Attribute
{
    public Type AssemblySpecifier { get; }

    public GenerateExtensionsForInternalTypesAttribute(Type assemblySpecifier)
    {
        this.AssemblySpecifier = assemblySpecifier;
    }
}