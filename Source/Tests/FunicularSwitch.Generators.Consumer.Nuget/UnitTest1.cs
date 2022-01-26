using System;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunicularSwitch;

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

    [ResultType(ErrorType = typeof(Error))]
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

namespace Test
{
    public static class Blus
    {
        public static void werlkj()
        {
            Result<int> i = null;

        }
    }

    [FunicularSwitch.Generators.UnionType]
    public abstract class UnionTest
    {
        public static readonly UnionTest Oins = new Oins_();
        public static readonly UnionTest Zwoi = new Zwoi_();

        public class Oins_ : UnionTest
        {
            public Oins_() : base(UnionCases.Oins)
            {
            }
        }

        public class Zwoi_ : UnionTest
        {
            public Zwoi_() : base(UnionCases.Zwoi)
            {
            }
        }

        internal enum UnionCases
        {
            Oins,
            Zwoi
        }

        internal UnionCases UnionCase { get; }
        UnionTest(UnionCases unionCase) => UnionCase = unionCase;

        public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
        bool Equals(UnionTest other) => UnionCase == other.UnionCase;

        public override bool Equals(object? obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((UnionTest)obj);
        }

        public override int GetHashCode() => (int)UnionCase;
    }
}

