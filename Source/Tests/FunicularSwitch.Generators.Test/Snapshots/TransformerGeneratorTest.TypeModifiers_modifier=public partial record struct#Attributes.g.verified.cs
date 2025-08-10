//HintName: Attributes.g.cs
#nullable enable
using System;
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal sealed class MonadAttribute : Attribute
    {
        public MonadAttribute(Type monadType)
        {
            MonadType = monadType;
        }

        public Type MonadType { get; }
    }

    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    internal sealed class MonadTransformerAttribute : Attribute
    {
        public MonadTransformerAttribute(Type monadType)
        {
            MonadType = monadType;
        }

        public Type MonadType { get; }
    }
}
