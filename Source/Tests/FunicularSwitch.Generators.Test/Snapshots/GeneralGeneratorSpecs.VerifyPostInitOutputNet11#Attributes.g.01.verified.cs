//HintName: Attributes.g.cs
#nullable enable
using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface | AttributeTargets.Struct, Inherited = false)]
    sealed class UnionTypeAttribute : Attribute
    {
        public CaseOrder CaseOrder { get; set; } = CaseOrder.Alphabetic;
        public bool StaticFactoryMethods { get; set; } = true;
    }

    /// <summary>
    /// Generate Match/Switch extension methods for all C# 15 <c>union</c> declarations defined in the assembly that contains AssemblySpecifier
    /// (the current assembly by default). Unions that are not public (or internal in the current assembly) are skipped.
    /// </summary>
    [AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
    class ExtendUnionsAttribute : Attribute
    {
        public Type AssemblySpecifier { get; }
        public ExtensionAccessibility Accessibility { get; set; } = ExtensionAccessibility.Public;

        public ExtendUnionsAttribute() => AssemblySpecifier = typeof(ExtendUnionsAttribute);

        public ExtendUnionsAttribute(Type assemblySpecifier) => AssemblySpecifier = assemblySpecifier;
    }

    enum CaseOrder
    {
        Alphabetic,
        AsDeclared,
        Explicit
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    sealed class UnionCaseAttribute : Attribute
    {
        public UnionCaseAttribute(int index) => Index = index;

        public int Index { get; }
    }
}