using FluentAssertions.Primitives;
//additional using directives

namespace FunicularSwitch.Generators.FluentAssertions.Templates
{
    internal partial class MyAssertions_UnionType : ObjectAssertions<MyUnionType, MyAssertions_UnionType>
    {
        public MyAssertions_UnionType(MyUnionType value) : base(value)
        {
        
        }
    }
}