//HintName: ResultTypeAttribute.g.cs
using System;
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ResultTypeAttribute : Attribute
    {
        public ResultTypeAttribute(Type errorType) => ErrorType = errorType;

        public Type ErrorType { get; }
    }
}