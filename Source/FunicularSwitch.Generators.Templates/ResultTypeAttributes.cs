using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    sealed class ResultTypeAttribute : Attribute
    {
        public ResultTypeAttribute() => ErrorType = typeof(string);
        public ResultTypeAttribute(Type errorType) => ErrorType = errorType;

        public Type ErrorType { get; set; }
    }

    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    sealed class MergeErrorAttribute : Attribute
    {
    }
}