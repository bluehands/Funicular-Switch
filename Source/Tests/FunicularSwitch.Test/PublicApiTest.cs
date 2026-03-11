using System.Reflection;
using PublicApiGenerator;

namespace FunicularSwitch.Test;

[TestClass]
[UsesVerify]
public partial class PublicApiTest
{
    [TestMethod]
    public Task PublicApiHasNotChanged()
    {
        // Given
        var assembly = typeof(Option).Assembly;
        
        // When
        var publicApi = assembly.GeneratePublicApi(
            new ApiGeneratorOptions()
            {
                ExcludeAttributes = [typeof(AssemblyMetadataAttribute).FullName!],
            });
        
        // Then
        return Verify(publicApi);
    }

}