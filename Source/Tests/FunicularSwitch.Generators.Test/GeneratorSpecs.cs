using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace FunicularSwitch.Generators.Test
{
    [TestClass]
    public class When_running_result_type_generator : with_result_type_generator
    {
        [TestMethod]
        public Task With_enum_error_type()
        {
            var code = @"
using FunicularSwitch.Generators;

namespace FunicularSwitch.Test;

[ResultType(errorType: typeof(MyError))]
public abstract partial class OperationResult<T>
{
}

public enum MyError 
{
    Generic,
    NotFound,
    Unauthorized
}
";
            return Verify(code);
        }
    }
}