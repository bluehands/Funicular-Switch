using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Enum)]
    sealed class EnumTypeAttribute : Attribute
    {
	    public EnumCaseOrder CaseOrder { get; set; } = EnumCaseOrder.AsDeclared;
	    public ExtensionAccessibility Accessibility { get; set; } = ExtensionAccessibility.Public;
    }
    
    enum EnumCaseOrder
    {
        Alphabetic,
        AsDeclared
    }

    /// <summary>
    /// Generate match methods for all enums defined in assembly that contains AssemblySpecifier.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    class ExtendEnumTypesAttribute : Attribute
    {
	    public Type AssemblySpecifier { get; }
	    public EnumCaseOrder CaseOrder { get; set; } = EnumCaseOrder.AsDeclared;
	    public ExtensionAccessibility Accessibility { get; set; } = ExtensionAccessibility.Public;

	    public ExtendEnumTypesAttribute() => AssemblySpecifier = typeof(ExtendEnumTypesAttribute);

	    public ExtendEnumTypesAttribute(Type assemblySpecifier)
	    {
		    AssemblySpecifier = assemblySpecifier;
	    }
    }

    /// <summary>
    /// Generate match methods for Type. Must be enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly)]
    class ExtendEnumTypeAttribute : Attribute
    {
	    public Type Type { get; }

	    public EnumCaseOrder CaseOrder { get; set; } = EnumCaseOrder.AsDeclared;

	    public ExtensionAccessibility Accessibility { get; set; } = ExtensionAccessibility.Public;

	    public ExtendEnumTypeAttribute(Type type)
	    {
		    Type = type;
	    }
    }

    enum ExtensionAccessibility
    {
	    Internal,
	    Public
    }
}