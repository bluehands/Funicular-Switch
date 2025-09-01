namespace FunicularSwitch.Transformers;


[AttributeUsage(AttributeTargets.Class, Inherited = false)]
public sealed class MonadTransformerAttribute : Attribute
{
    public MonadTransformerAttribute(Type monadType) { }
}
