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
        var publicApi = assembly.GeneratePublicApi();
        
        // Then
        return Verify(publicApi);
    }

}