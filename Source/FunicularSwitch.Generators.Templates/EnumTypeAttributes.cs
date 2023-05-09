using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Enum)]
    sealed class EnumTypeAttribute : Attribute
    {
        public CaseOrder CaseOrder { get; set; } = CaseOrder.AsDeclared;
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    sealed class EnumCaseAttribute : Attribute
    {
        public EnumCaseAttribute(int index) => Index = index;

        public int Index { get; }
    }
}