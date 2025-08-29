using System;

namespace FunicularSwitch.Generators
{
    [AttributeUsage((AttributeTargets)12, AllowMultiple = false, Inherited = false)]
    internal class MonadAttribute : Attribute
    {
        public MonadAttribute(FunicularSwitch.Generators.Monad.MonadAttribute original) { }
    }
}
