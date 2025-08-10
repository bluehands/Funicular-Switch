using System;

namespace FunicularSwitch.Generators
{
    [AttributeUsage((AttributeTargets)12, AllowMultiple = false, Inherited = true)]
    internal class TransformMonadAttribute : Attribute
    {
        public TransformMonadAttribute(System.Type monadType, System.Type transformerType) { }
    }
}
