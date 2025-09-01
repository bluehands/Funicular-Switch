using System;

namespace FunicularSwitch.Generators
{
    /// <summary>
    /// This generator is still considered experimental and might break.
    /// Please open any issues you may find in <see href="https://github.com/bluehands/Funicular-Switch/issues">the GitHub repo</see>.
    /// </summary>
    [AttributeUsage((AttributeTargets)12, AllowMultiple = false, Inherited = false)]
    internal class TransformMonadAttribute : Attribute
    {
        public TransformMonadAttribute(System.Type monadType, System.Type transformerType, params System.Type[] extraTransformerTypes) { }
    }
}
