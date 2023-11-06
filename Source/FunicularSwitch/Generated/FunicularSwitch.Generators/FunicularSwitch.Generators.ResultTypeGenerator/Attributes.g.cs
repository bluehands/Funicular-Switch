using System;

// ReSharper disable once CheckNamespace
namespace FunicularSwitch.Generators
{
	/// <summary>
	/// Mark an abstract partial type with a single generic argument with the ResultType attribute.
	/// This type from now on has Ok | Error semantics with map and bind operations.
	/// </summary>
    [AttributeUsage(AttributeTargets.Class, Inherited = false)]
    sealed class ResultTypeAttribute : Attribute
    {
        public ResultTypeAttribute() => ErrorType = typeof(string);
        public ResultTypeAttribute(Type errorType) => ErrorType = errorType;

        public Type ErrorType { get; set; }
    }

    /// <summary>
    /// Mark a static method or a member method or you error type with the MergeErrorAttribute attribute.
    /// Static signature: TError -> TError -> TError. Member signature: TError -> TError
    /// We are now able to collect errors and methods like Validate, Aggregate, FirstOk that are useful to combine results are generated.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    sealed class MergeErrorAttribute : Attribute
    {
    }

    /// <summary>
    /// Mark a static method with the ExceptionToError attribute.
    /// Signature: Exception -> TError
    /// This method is always called, when an exception happens in a bind operation.
    /// So a call like result.Map(i => i/0) will return an Error produced by the factory method instead of throwing the DivisionByZero exception.
    /// </summary>
    [AttributeUsage(AttributeTargets.Method, Inherited = false)]
    sealed class ExceptionToError : Attribute
    {
    }
}