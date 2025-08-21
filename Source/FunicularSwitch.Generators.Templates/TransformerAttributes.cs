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

    internal interface Monad<A>
    {
        public Monad<B> Bind<B>(Func<A, Monad<B>> fn);

        public Monad<B> Return<B>(B value);
    }

    internal static class MonadExtension
    {
        public static Monad<B> Map<A, B>(this Monad<A> ma, Func<A, B> fn) => ma.Bind(a => ma.Return(fn(a)));
    }
}