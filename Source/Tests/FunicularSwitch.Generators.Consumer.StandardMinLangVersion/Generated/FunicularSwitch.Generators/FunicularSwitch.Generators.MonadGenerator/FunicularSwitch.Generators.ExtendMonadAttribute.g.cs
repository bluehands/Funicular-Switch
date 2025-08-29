using System;

namespace FunicularSwitch.Generators
{
    [AttributeUsage((AttributeTargets)12, AllowMultiple = false, Inherited = false)]
    internal class ExtendMonadAttribute : Attribute
    {
        public ExtendMonadAttribute(FunicularSwitch.Generators.Monad.ExtendMonadAttribute original) { }
    }
}
