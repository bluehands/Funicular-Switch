using System.Collections.Generic;
using System.Linq;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunicularSwitch.Extensions;

namespace FunicularSwitch.Test
{
    [TestClass]
    public class EnumerableExtensionSpecs
    {
        [TestMethod]
        public void EnumerableConcatStillWorks()
        {
            Enumerable.Range(0, 2).Concat(Enumerable.Range(2, 2)).Should().BeEquivalentTo(Enumerable.Range(0, 4));
            Enumerable.Range(0, 2).Concat(Enumerable.Range(2, 2).ToArray()).Should().BeEquivalentTo(Enumerable.Range(0, 4));

            0.Yield().Concat(new[] { 1, 2 }).Should().BeEquivalentTo(new[]{0, 1, 2});
        }
    }

    [TestClass]
    public class DictionaryExtensionSpecs
    {
        Dictionary<string, int> m_Lookup = null!;

        [TestInitialize]
        public void Init()
        {
            m_Lookup = new Dictionary<string, int> { { "one", 1 } };
        }

        [TestMethod]
        public void TestGetExistingValue()
        {
            var one = m_Lookup.TryGetValue("one");
            one.IsSome().Should().BeTrue();
            one.GetValueOrThrow().Should().Be(1);
        }

        [TestMethod]
        public void TestGetNonExistingValue()
        {
            var one = m_Lookup.TryGetValue("two");
            one.IsNone().Should().BeTrue();
        }
    }
}