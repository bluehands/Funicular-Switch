using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Interface, Inherited = false)]
    sealed class UnionTypeAttribute : Attribute
    {
        public CaseOrder CaseOrder { get; set; } = CaseOrder.Alphabetic;
        public bool StaticFactoryMethods { get; set; } = true;
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