using System;
//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions;

[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true)]
public class GenerateFluentAssertionsForAttribute : Attribute
{
    public GenerateFluentAssertionsForAttribute(Type type)
    {
        Type = type;
    }

    public Type Type { get; }
}