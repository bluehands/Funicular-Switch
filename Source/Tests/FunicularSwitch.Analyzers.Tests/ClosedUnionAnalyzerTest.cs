using FunicularSwitch.Analyzers.CodeFixProviders;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.Diagnostics;

namespace FunicularSwitch.Analyzers.Tests;

public class ClosedUnionAnalyzerTest : VerifyAnalyzer
{
    public ClosedUnionAnalyzerTest() : base() { }

    // The generator emits UnionTypeAttribute into the consuming assembly; tests declare it themselves.
    private const string Attributes =
        """
        using FunicularSwitch.Generators;
        namespace FunicularSwitch.Generators
        {
            [System.AttributeUsage(System.AttributeTargets.Class | System.AttributeTargets.Interface)]
            public sealed class UnionTypeAttribute : System.Attribute { }
        }

        """;
    private Task VerifyClosed(string code, string[] expectedIds, LanguageVersion languageVersion = LanguageVersion.Preview, bool netStandard = false, [System.Runtime.CompilerServices.CallerMemberName] string caller = "") =>
        Verify(
            Attributes + code,
            new ClosedUnionAnalyzer(),
            new CompositeFixProvider(),
            d => d.Select(x => x.Id).Should().BeEquivalentTo(expectedIds),
            languageVersion: languageVersion,
            netStandard: netStandard,
            callingMethod: caller);
    [Fact]
    public Task AbstractRecord_SuggestsClosed_FixMakesClosed() =>
        VerifyClosed(
            """
            [UnionType]
            public abstract record Shape
            {
                public record Circle(double R) : Shape;
                public sealed record Square(double S) : Shape;
            }
            """,
            ["FS0002"]);

    [Fact]
    public Task AbstractClass_SuggestsClosed() =>
        VerifyClosed(
            """
            [UnionType]
            public abstract class Shape
            {
                public sealed class Circle : Shape { }
            }
            """,
            ["FS0002"]);

    [Fact]
    public Task PartialDeclarations_AllAreMadeClosed() =>
        VerifyClosed(
            """
            [UnionType]
            public abstract partial record Shape;
            public abstract partial record Shape { public sealed record Circle : Shape; }
            """,
            ["FS0002"]);

    [Fact]
    public Task GenericBase_SuggestsClosed() =>
        VerifyClosed(
            """
            [UnionType]
            public abstract record Tree<T>
            {
                public sealed record Leaf(T Value) : Tree<T>;
                public sealed record Node(Tree<T> Left, Tree<T> Right) : Tree<T>;
            }
            """,
            ["FS0002"]);

    [Fact]
    public Task GenericCaseWithFreeTypeParameter_NoSuggestion() =>
        VerifyClosed(
            """
            [UnionType]
            public abstract record Tree<T>;
            public sealed record Leaf<T>(T Value) : Tree<T>;
            public sealed record Constant<U>(U Value) : Tree<int>;
            """, []);

    [Fact]
    public Task AlreadyClosed_NoSuggestion() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            public sealed record Circle : Shape;
            """, []);

    [Fact]
    public Task LanguageVersion14_NoDiagnostic() =>
        VerifyClosed(
            """
            [UnionType]
            public abstract record Shape;
            public sealed record Circle : Shape;
            """,
            [], LanguageVersion.CSharp14);

    [Fact]
    public Task InterfaceNonAbstractAndNonUnion_NoDiagnostic() =>
        VerifyClosed(
            """
            [UnionType]
            public interface IShape;
            [UnionType]
            public record Open;
            public abstract record NotAUnion;
            """, []);

    [Fact]
    public Task ClosedWithNonClosedIntermediate_WarnsAndFixMakesClosed() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            public abstract record Round : Shape;
            public sealed record Circle : Round;
            public sealed record Ellipse : Round;
            """,
            ["FS0003"]);

    [Fact]
    public Task ClosedIntermediate_NoWarning() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            public closed record Round : Shape;
            public sealed record Circle : Round;
            """, []);

    [Fact]
    public Task ClosedWithUnsealedCase_InfoAndFixAddsSealed() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            public record Circle : Shape;
            """,
            ["FS0004"]);

    [Fact]
    public Task ClosedWithUnsealedCase_FixKeepsXmlDocAndIndentationOnce() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            /// <summary>A circle.</summary>
            public record Circle : Shape;
            /// <summary>No modifiers at all.</summary>
            record Square : Shape;
            """,
            ["FS0004", "FS0004"]);

    [Fact]
    public Task ClosedWithUnsealedCase_FixWithAttributesAndNoModifiers() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            /// <summary>Doc.</summary>
            [System.Obsolete]
            record Circle : Shape;
            """,
            ["FS0004"]);

    [Fact]
    public Task ClosedWithUnsealedCase_FixOnPartialOnly() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            /// <summary>Doc.</summary>
            partial record Circle : Shape;
            """,
            ["FS0004"]);

    [Fact]
    public Task ClosedWithNonSealedConcreteCaseHavingConcreteSubclass_NoDiagnostic() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            public record Circle : Shape;
            public sealed record UnitCircle : Circle;
            """, []);

    [Fact]
    public Task ClosedAbstractIntermediateWithoutDescendants_NoDiagnostic() =>
        VerifyClosed(
            """
            [UnionType]
            public closed record Shape;
            public closed record Round : Shape;
            public sealed record Square : Shape;
            """, []);

    [Fact]
    public void DiagnosticNeverLandsInGeneratedFile()
    {
        var options = new CSharpParseOptions(LanguageVersion.Preview);
        var user = CSharpSyntaxTree.ParseText(Attributes + "[UnionType] public closed partial record Shape; public partial record Circle : Shape;", options, "User.cs");
        var generated = CSharpSyntaxTree.ParseText("// <auto-generated/>\npublic partial record Circle;", options, "Other.g.cs");
        var compilation = CSharpCompilation.Create("x", [generated, user], ReferenceAssemblies(), new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        var diagnostics = compilation.WithAnalyzers([new ClosedUnionAnalyzer()]).GetAnalyzerDiagnosticsAsync().Result;
        diagnostics.Should().ContainSingle().Which.Location.SourceTree!.FilePath.Should().Be("User.cs");

        // declared only in the generated file: nothing at all
        var onlyGenerated = CSharpCompilation.Create("y", [user.WithFilePath("Shape.cs"), CSharpSyntaxTree.ParseText("// <auto-generated/>\npublic record Circle2 : Shape;", options, "Gen.g.cs")],
            ReferenceAssemblies(), new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        onlyGenerated.WithAnalyzers([new ClosedUnionAnalyzer()]).GetAnalyzerDiagnosticsAsync().Result
            .Should().OnlyContain(d => d.Location.SourceTree!.FilePath == "Shape.cs");
    }

    [Fact]
    public Task StjAvailable_SecondActionAddsJsonPolymorphic() =>
        Verify(
            Attributes +
            """
            [UnionType]
            public abstract record Shape;
            public sealed record Circle : Shape;
            """,
            new ClosedUnionAnalyzer(),
            new MakeClosedCodeFixProvider(),
            d => d.Should().ContainSingle().Which.Id.Should().Be("FS0002"),
            actionTitleContains: "JsonPolymorphic");

    [Fact]
    public Task StjAvailable_PartialDeclarationsInSameFile_AttributeOnlyOnFirst() =>
        Verify(
            Attributes +
            """
            [UnionType]
            public abstract partial record Shape;
            public abstract partial record Shape { public sealed record Circle : Shape; }
            """,
            new ClosedUnionAnalyzer(),
            new MakeClosedCodeFixProvider(),
            d => d.Should().ContainSingle().Which.Id.Should().Be("FS0002"),
            actionTitleContains: "JsonPolymorphic");

    [Fact]
    public Task StjAlreadyConfigured_OnlyPlainAction() =>
        Verify(
            Attributes +
            """
            [UnionType]
            [System.Text.Json.Serialization.JsonPolymorphic]
            public abstract record Shape;
            public sealed record Circle : Shape;
            """,
            new ClosedUnionAnalyzer(),
            new MakeClosedCodeFixProvider(),
            d => d.Should().ContainSingle(),
            (_, action) => action.Title.Should().NotContain("JsonPolymorphic"));

    [Fact]
    public Task Csharp15ButClosedSupportTypesMissing_NoSuggestion() =>
        VerifyClosed(
            """
            [UnionType]
            public abstract record Shape;
            public sealed record Circle : Shape;
            """,
            [], LanguageVersion.Preview, netStandard: true);
    [Fact]
    public void LanguageFeatures_SupportsClosed_DependsOnLanguageVersion()
    {
        static bool Supports(LanguageVersion v) => LanguageFeatures.SupportsClosed(
            CSharpCompilation.Create("x", references: ReferenceAssemblies(), options: new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary), syntaxTrees: [CSharpSyntaxTree.ParseText("", new CSharpParseOptions(v))]));

        Supports(LanguageVersion.CSharp14).Should().BeFalse();
        Supports(LanguageVersion.Preview).Should().BeTrue();
        Supports(LanguageVersion.Latest).Should().Be(LanguageVersion.Latest.MapSpecifiedToEffectiveVersion() >= (LanguageVersion)1500);
    }

    [Fact]
    public void LanguageFeatures_IsClosed_WorksForSourceAndMetadataSymbols()
    {
        var options = new CSharpParseOptions(LanguageVersion.Preview);
        var library = CSharpCompilation.Create("lib",
            [CSharpSyntaxTree.ParseText("public closed class A; public abstract class B;", options)],
            ReferenceAssemblies(), new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));
        library.GetDiagnostics().Where(d => d.Severity == DiagnosticSeverity.Error).Should().BeEmpty();

        LanguageFeatures.IsClosed(library.GetTypeByMetadataName("A")!).Should().BeTrue();
        LanguageFeatures.IsClosed(library.GetTypeByMetadataName("B")!).Should().BeFalse();

        var consumer = CSharpCompilation.Create("consumer",
            [CSharpSyntaxTree.ParseText("", options)],
            [.. ReferenceAssemblies(), MetadataReference.CreateFromImage(Emit(library))]);
        var metadataA = consumer.GetTypeByMetadataName("A")!;
        LanguageFeatures.IsClosed(metadataA).Should().BeTrue();
        LanguageFeatures.IsClosed(consumer.GetTypeByMetadataName("B")!).Should().BeFalse();
    }
    private static byte[] Emit(Compilation c)
    {
        using var ms = new MemoryStream();
        c.Emit(ms).Success.Should().BeTrue();
        return ms.ToArray();
    }

    /// <summary>Dispatches to the right provider by diagnostic id, like the IDE would.</summary>
    private sealed class CompositeFixProvider : Microsoft.CodeAnalysis.CodeFixes.CodeFixProvider
    {
        private readonly MakeClosedCodeFixProvider makeClosed = new();
        private readonly AddSealedCodeFixProvider addSealed = new();

        public override System.Collections.Immutable.ImmutableArray<string> FixableDiagnosticIds =>
            makeClosed.FixableDiagnosticIds.AddRange(addSealed.FixableDiagnosticIds);

        public override Task RegisterCodeFixesAsync(Microsoft.CodeAnalysis.CodeFixes.CodeFixContext context) =>
            context.Diagnostics[0].Id == "FS0004"
                ? addSealed.RegisterCodeFixesAsync(context)
                : makeClosed.RegisterCodeFixesAsync(context);
    }
}












