using FluentAssertions;
using FunicularSwitch.Generators.Analyzers;
using FunicularSwitch.Generators.CodeFixProviders;

namespace FunicularSwitch.Generators.Test;

[TestClass]
public class PolyTypeAnalyzerTests : VerifyAnalyzer
{
    public PolyTypeAnalyzerTests() : base()
    {
    }

    [TestMethod]
    public async Task PolyTypeDerivedAttribute_MissingIsRecognized_FixIsApplied()
    {
        var code =
            """
            using FunicularSwitch.Generators;
            
            [UnionType]
            public abstract partial record BaseType
            {
                public sealed record DerivedType_() : BaseType;
                public sealed record DerivedType2_() : BaseType;
            }
            """;
        await Verify(
            code,
            new PolyTypeAnalyzer(),
            new PolyTypeCodeFixProvider(),
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0010"),
            hasPolytypeReference: true);
    }
    [TestMethod]
    public async Task PolyTypeDerivedAttribute_PartialMissingIsRecognized_FixIsApplied()
    {
        var code =
            """
            using FunicularSwitch.Generators;
            
            [UnionType]
            [PolyType.DerivedTypeShape(typeof(BaseType.DerivedType_))]
            public abstract partial record BaseType
            {
                public sealed record DerivedType_() : BaseType;
                public sealed record DerivedType2_() : BaseType;
            }
            """;
        await Verify(
            code,
            new PolyTypeAnalyzer(),
            new PolyTypeCodeFixProvider(),
            diagnostic => diagnostic.Should().Contain(s => s.Id == "FS0010"),
            hasPolytypeReference: true);
    }

}