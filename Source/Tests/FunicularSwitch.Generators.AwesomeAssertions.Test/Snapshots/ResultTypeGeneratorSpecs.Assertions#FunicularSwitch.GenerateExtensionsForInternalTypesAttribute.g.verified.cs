//HintName: FunicularSwitch.GenerateExtensionsForInternalTypesAttribute.g.cs
#nullable enable
using System;

namespace FunicularSwitch;

[AttributeUsage(validOn: AttributeTargets.Assembly, AllowMultiple = true)]
public class GenerateExtensionsForInternalTypesAttribute : Attribute
{
    public Type AssemblySpecifier { get; }

    public GenerateExtensionsForInternalTypesAttribute(Type assemblySpecifier)
    {
        this.AssemblySpecifier = assemblySpecifier;
    }
}