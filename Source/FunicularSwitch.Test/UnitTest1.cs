using System.Net.Mime;
using System.Reflection;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test
{
    public class Test
    {
        public static void VoidMethod()
        {

        }
    }

    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var m = typeof(Test).GetMethods(BindingFlags.Static | BindingFlags.Public);
        }
    }
}
