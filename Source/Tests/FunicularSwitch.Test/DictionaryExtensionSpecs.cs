using System.Collections.Generic;
using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using FunicularSwitch.Extensions;

namespace FunicularSwitch.Test
{
    [TestClass]
    public class DictionaryExtensionSpecs
    {
        Dictionary<string, int> m_Lookup;

        [TestInitialize]
        public void Init()
        {
            m_Lookup = new Dictionary<string, int> {{"one", 1}};
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
