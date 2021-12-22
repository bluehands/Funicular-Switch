using System;
// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    public class ResultTypeAttribute : Attribute
    {
        public ResultTypeAttribute(Type errorType) => ErrorType = errorType;

        public Type ErrorType { get; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    public class MergeErrorAttribute : Attribute
    {
    }
}