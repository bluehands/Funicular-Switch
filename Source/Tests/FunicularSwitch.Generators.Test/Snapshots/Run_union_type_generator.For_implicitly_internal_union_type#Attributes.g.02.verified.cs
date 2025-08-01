﻿//HintName: Attributes.g.cs
#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Enum)]
    sealed class ExtendedEnumAttribute : Attribute
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
    class ExtendEnumsAttribute : Attribute
    {
	    public Type AssemblySpecifier { get; }
	    public EnumCaseOrder CaseOrder { get; set; } = EnumCaseOrder.AsDeclared;
	    public ExtensionAccessibility Accessibility { get; set; } = ExtensionAccessibility.Public;

	    public ExtendEnumsAttribute() => AssemblySpecifier = typeof(ExtendEnumsAttribute);

	    public ExtendEnumsAttribute(Type assemblySpecifier)
	    {
		    AssemblySpecifier = assemblySpecifier;
	    }
    }

    /// <summary>
    /// Generate match methods for Type. Must be enum.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    class ExtendEnumAttribute : Attribute
    {
	    public Type Type { get; }

	    public EnumCaseOrder CaseOrder { get; set; } = EnumCaseOrder.AsDeclared;

	    public ExtensionAccessibility Accessibility { get; set; } = ExtensionAccessibility.Public;

	    public ExtendEnumAttribute(Type type)
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