using FluentAssertions;
using FunicularSwitch.Generators.Analyzers;
using FunicularSwitch.Generators.CodeFixProviders;

namespace FunicularSwitch.Generators.Test.AnalyzerTests;

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
            
            namespace FunicularSwitch.Test;
            
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
            
            namespace FunicularSwitch.Test;
            
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
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0010"),
            hasPolytypeReference: true);
    }

    [TestMethod]
    public async Task PolyTypeDerivedAttribute_PartialMissingWithGenerics_FixIsApplied()
    {
        var code =
            """
            using FunicularSwitch.Generators;
            
            namespace FunicularSwitch.Test.Generic;
            
            [UnionType(CaseOrder = CaseOrder.AsDeclared)]
            [PolyType.DerivedTypeShape(typeof(GenericResult<,>.Ok_))]
            public abstract partial record GenericResult<T, TFailure>(bool IsOk)
            {
                public record Ok_(T Value) : GenericResult<T, TFailure>(true);
                public record Error_(TFailure Failure) : GenericResult<T, TFailure>(false);
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
    public async Task PolyTypeDerivedAttribute_HasPolyTypeReferenced_NamespaceIsOmitted()
    {
        var code =
            """
            using FunicularSwitch.Generators;
            using PolyType;
            
            namespace FunicularSwitch.Test;
            
            [UnionType]
            public abstract partial record BaseType
            {
                public sealed record DerivedType_() : BaseType;
                public sealed record DerivedType2_() : BaseType;
            }
            """;;
        await Verify(
            code,
            new PolyTypeAnalyzer(),
            new PolyTypeCodeFixProvider(),
            diagnostic => diagnostic.Should().ContainSingle().Which.Id.Should().Be("FS0010"),
            hasPolytypeReference: true);
    }

}