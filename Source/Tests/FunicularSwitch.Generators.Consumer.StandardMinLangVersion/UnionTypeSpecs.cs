using System;
using FunicularSwitch.Generators;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace StandardMinLangVersion
{
    [TestClass]
    public class StaticInitializersTest
    {
        [TestMethod]
        public void ForNestedClassCases()
        {
            var case1 = MyUnion.Case1();
            var case2 = MyUnion.Case2();

            string Print(MyUnion union) => union.Match(
                case1: _ => "Case1",
                case2: _ => "Case2"
            );

            Console.WriteLine(Print(case1));
            Console.WriteLine(Print(case2));
        }

        [TestMethod]
        public void ResultGenerator()
        {
            var ok = Result.Ok(42);
            Console.WriteLine(ok.Match(value => $"Ok: {value}", error => error));
        }
    }

    [UnionType]
    public abstract partial class MyUnion
    {
        public class Case1_ : MyUnion {}
        public class MyUnionCase2 : MyUnion {}
    }

    [ResultType(ErrorType = typeof(string))]
    public abstract partial class Result<T>
    {
    }

    public static class ResultExtensions
    {
        [MergeError]
        public static string MergeError(this string e1, string e2) => e1 + e2;
    }
}


