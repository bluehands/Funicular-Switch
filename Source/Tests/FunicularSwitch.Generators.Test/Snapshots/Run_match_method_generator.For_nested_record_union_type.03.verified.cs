//HintName: Attributes.g.cs
using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Enum)]
    sealed class EnumTypeAttribute : Attribute
    {
        public CaseOrder CaseOrder { get; set; } = CaseOrder.AsDeclared;
    }
}