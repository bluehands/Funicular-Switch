using System.Threading.Tasks;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using VerifyMSTest;

namespace FunicularSwitch.Generators.Test;

public abstract class with_result_type_generator : VerifyBase
{
    public Task Verify(string source)
    {
        var syntaxTree = CSharpSyntaxTree.ParseText(source);

        // Create references for assemblies we require
        // We could add multiple references if required
        var references = new[]
        {
            MetadataReference.CreateFromFile(typeof(object).Assembly.Location)
        };

        var compilation = CSharpCompilation.Create(
            assemblyName: "Tests",
            syntaxTrees: new[] { syntaxTree },
            references: references);

        var generator = new ResultTypeGenerator();

        GeneratorDriver driver = CSharpGeneratorDriver.Create(generator);

        driver = driver.RunGenerators(compilation);

        return Verify(driver);
    }
}