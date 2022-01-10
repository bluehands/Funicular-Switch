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
            TestResult<int> result = 42;
            result.IsOk.Should().BeTrue();

            var (i, s) = result.Aggregate(TestResult.Ok("Hey result")).GetValueOrThrow();
        }
    }

    public class Error
    {
        
    }

    public static class ErrorExtensions
    {
        [MergeError]
        public static Error Merge(this Error me, Error other) => new();
    }

    [ResultType(typeof(Error))]
    public abstract partial class TestResult<T>
    {
    }

    
}