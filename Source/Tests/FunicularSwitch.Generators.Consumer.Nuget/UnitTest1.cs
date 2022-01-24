using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Consumer.Nuget
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            Result<int> result = 42;
            result.IsOk.Should().BeTrue();

            var (i, s) = result.Aggregate(Result.Ok("Hey result")).GetValueOrThrow();
        }
    }

    public class Error
    {
        //this could be a union type empowered by Switchyard :)
    }

    [ResultType(errorType: typeof(Error))]
    public abstract partial class Result<T>
    {
    }

    //Optional if errors can be combined (produces Aggregate methods to merge multiple results)
    public static class ErrorExtensions
    {
        [MergeError]
        public static Error Merge(this Error me, Error other) => throw new NotImplementedException();
    }

    
}