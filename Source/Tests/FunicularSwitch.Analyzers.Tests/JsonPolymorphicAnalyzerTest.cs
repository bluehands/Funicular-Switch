namespace FunicularSwitch.Analyzers.Tests;

public class JsonPolymorphicAnalyzerTest : VerifyAnalyzer
{
    public JsonPolymorphicAnalyzerTest() : base() { }

    private const string Prelude =
        """
        using FunicularSwitch.Generators;
        using System.Text.Json.Serialization;
        namespace FunicularSwitch.Generators
        {
            [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Interface)]
            public sealed class UnionTypeAttribute : System.Attribute { }
        }

        """;
    private Task Check(string code, params string[] ids) =>
        Verify(Prelude + code, new JsonPolymorphicAnalyzer(), null, d => d.Select(x => x.Id).Should().BeEquivalentTo(ids));

    [Fact]
    public Task ManualDerivedTypeList_OnClosedUnion_IsReported() =>
        Check(
            """
            [UnionType]
            [JsonPolymorphic]
            [JsonDerivedType(typeof(Circle), "circle")]
            public closed record Shape;
            public sealed record Circle : Shape;
            """,
            "FS0005");

    [Fact]
    public Task InferClosedTypePolymorphism_NotReported() =>
        Check(
            """
            [UnionType]
            [JsonPolymorphic(InferClosedTypePolymorphism = true)]
            public closed record Shape;
            public sealed record Circle : Shape;
            """);

    [Fact]
    public Task NotClosed_NotReported() =>
        Check(
            """
            [UnionType]
            [JsonPolymorphic]
            [JsonDerivedType(typeof(Circle), "circle")]
            public abstract record Shape;
            public sealed record Circle : Shape;
            """);

    [Fact]
    public Task CaseMissingFromList_IsReported() =>
        Check(
            """
            [UnionType]
            [JsonPolymorphic]
            [JsonDerivedType(typeof(Circle), "circle")]
            public closed record Shape;
            public sealed record Circle : Shape;
            public sealed record Square : Shape;
            """,
            "FS0005", "FS0006");

    [Fact]
    public Task AllCasesListed_OnlySuggestionReported() =>
        Check(
            """
            [UnionType]
            [JsonPolymorphic]
            [JsonDerivedType(typeof(Circle), "circle")]
            [JsonDerivedType(typeof(Square), "square")]
            public closed record Shape;
            public sealed record Circle : Shape;
            public sealed record Square : Shape;
            """,
            "FS0005");
}
