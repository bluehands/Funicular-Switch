//HintName: FunicularSwitch.Generators.TransformMonadAttribute.g.cs
using System;

namespace FunicularSwitch.Generators
{
    [AttributeUsage((AttributeTargets)12, AllowMultiple = false, Inherited = false)]
    internal class TransformMonadAttribute : Attribute
    {
        public TransformMonadAttribute(System.Type monadType, params System.Type[] transformerTypes) { }
    }
}
