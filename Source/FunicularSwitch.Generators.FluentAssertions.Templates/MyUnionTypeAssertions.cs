using FluentAssertions.Primitives;
//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal partial class MyUnionTypeAssertions : ObjectAssertions<MyUnionType, MyUnionTypeAssertions>
    {
        public MyUnionTypeAssertions(MyUnionType value) : base(value)
        {
        
        }
    }
}