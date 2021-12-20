using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentAssertions;
using System.Globalization;
using FunicularSwitch.Extensions;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Test
{
    [TestClass]
    public class When_testdesciption
    {
        [TestMethod]
        public void MyResultTest()
        {
            static OperationResult<decimal> Divide(decimal i, decimal divisor) => divisor == 0
                ? OperationResult.Error<decimal>(MyError.Generic("Division by zero"))
                : i / divisor;

            OperationResult<int> result = 42;

            var calc = result.Bind(i => Divide(i, 0)).Map(i => (i * 2).ToString(CultureInfo.InvariantCulture));
            calc.Should().BeEquivalentTo(OperationResult<string>.Error(MyError.Generic("Division by zero")));

            //var t = calc.Aggregate(MyResult.Error<int>(MyError.NotFound));
        }
    }

    public class ResultTypeAttribute : Attribute
    {
        public ResultTypeAttribute(Type errorType) => ErrorType = errorType;

        public Type ErrorType { get; }
    }

    [ResultType(errorType: typeof(MyError))]
    public abstract partial class OperationResult<T>
    {
    }
}
