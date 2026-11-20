using System.Collections.Immutable;
using FluentAssertions;
using FunicularSwitch.Transformers;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

namespace FunicularSwitch.Generators.Test;

/// <summary>
/// Regression tests for incremental caching: edits that do not change what a generator emits must leave
/// its pipeline cached, and a semantic edit must only touch the outputs it affects.
/// </summary>
[TestClass]
public class Incremental_caching
{
    static readonly string[] OutputSteps =
    [
        TrackingNames.UnionOutput, TrackingNames.ExtendedUnionOutput, TrackingNames.ResultTypeOutput, TrackingNames.EnumOutput
    ];

    const string Header = "using FunicularSwitch.Generators;\nnamespace Test;\n";

    static readonly (string path, string text)[] Files =
    [
        ("Assembly.cs", "using FunicularSwitch.Generators;\n[assembly: ExtendUnions]\n"),
        ("Cat.cs", "namespace Test;\npublic record Cat(string Name);\npublic record Dog(string Name);\npublic record Fish(string Name);\n"),
        ("Native1.cs", Header + "public union Pet(Cat, Dog);\n"),
        ("Native2.cs", Header + "public union Animal(Cat, Dog, Fish);\n"),
        ("Explicit.cs", Header + "[UnionType]\npublic union Explicit(Cat, Dog);\n"),
        ("Both.cs", Header + "[UnionType]\npublic union Both(Cat, Fish);\n"),
        ("Hierarchy.cs", Header + "[UnionType]\npublic abstract partial record Shape { public sealed partial record Circle : Shape; }\n"),
        ("Result.cs", Header + "[ResultType(ErrorType = typeof(MyError))]\npublic abstract partial class Res<T> { }\n"),
        ("MyError.cs", "namespace Test;\npublic class MyError { }\n"),
        ("Enum.cs", Header + "[ExtendedEnum]\npublic enum Color { Red, Green }\n"),
        ("Unrelated.cs", "namespace Test;\npublic class Unrelated { public int X; }\n"),
    ];

    static CSharpCompilation CreateCompilation((string path, string text)[] files)
    {
        var refDir = BaseVerifySourceGenerators.Net11ReferenceDirectory();
        var references = Directory.GetFiles(refDir, "*.dll")
            .Append(typeof(Monad<>).Assembly.Location)
            .Select(f => MetadataReference.CreateFromFile(f));
        var parseOptions = new CSharpParseOptions(LanguageVersion.CSharp15);
        return CSharpCompilation.Create("Tests",
            files.Select(f => CSharpSyntaxTree.ParseText(f.text, parseOptions, f.path)),
            references,
            new(OutputKind.DynamicallyLinkedLibrary, nullableContextOptions: NullableContextOptions.Enable));
    }

    static (CSharpCompilation compilation, GeneratorDriver driver) Initial((string path, string text)[] files)
    {
        var compilation = CreateCompilation(files);
        GeneratorDriver driver = CSharpGeneratorDriver.Create(
            generators: [new ResultTypeGenerator().AsSourceGenerator(), new UnionTypeGenerator().AsSourceGenerator(), new EnumTypeGenerator().AsSourceGenerator()],
            additionalTexts: null,
            parseOptions: new CSharpParseOptions(LanguageVersion.CSharp15),
            optionsProvider: null,
            driverOptions: new GeneratorDriverOptions(IncrementalGeneratorOutputKind.None, trackIncrementalGeneratorSteps: true));
        driver = driver.RunGenerators(compilation);
        return (compilation, driver);
    }

    static (string path, string text)[] Edit(string path, Func<string, string> edit) =>
        Files.Select(f => f.path == path ? (f.path, edit(f.text)) : f).ToArray();

    sealed record Run(
        Dictionary<string, List<IncrementalGeneratorRunStep>> Steps,
        Dictionary<string, string> Sources)
    {
        public IEnumerable<IncrementalStepRunReason> Reasons(string step) =>
            Steps.TryGetValue(step, out var s) ? s.SelectMany(x => x.Outputs).Select(o => o.Reason) : [];

        public int Count(string step, IncrementalStepRunReason reason) => Reasons(step).Count(r => r == reason);
        public string Describe(string step) => string.Join(",", Reasons(step));
    }

    static Run Rerun((string path, string text)[] before, (string path, string text)[] after)
    {
        var (compilation, driver) = Initial(before);
        var oldSources = Sources(driver);
        var newCompilation = compilation.RemoveAllSyntaxTrees().AddSyntaxTrees(CreateCompilation(after).SyntaxTrees);
        driver = driver.RunGenerators(newCompilation);
        var result = driver.GetRunResult();

        var steps = new Dictionary<string, List<IncrementalGeneratorRunStep>>();
        foreach (var r in result.Results)
        foreach (var (name, runSteps) in r.TrackedSteps)
        {
            if (!steps.TryGetValue(name, out var list)) steps[name] = list = [];
            list.AddRange(runSteps);
        }

        var newSources = Sources(driver);
        newSources.Should().BeEquivalentTo(Sources(Initial(after).driver), "incremental output must equal a from-scratch run (stale cache otherwise)");
        var changed = newSources.Where(kv => !oldSources.TryGetValue(kv.Key, out var o) || o != kv.Value).ToDictionary(kv => kv.Key, kv => kv.Value);
        return new(steps, changed);
    }

    static Dictionary<string, string> Sources(GeneratorDriver driver) =>
        driver.GetRunResult().Results
            .SelectMany(r => r.GeneratedSources)
            .Where(s => s.HintName != "Attributes.g.cs")
            .ToDictionary(s => s.HintName, s => s.SourceText.ToString());

    static void AssertEverythingCached(Run run)
    {
        foreach (var step in OutputSteps)
        {
            run.Steps.Should().ContainKey(step);
            run.Reasons(step).Should().OnlyContain(r => r == IncrementalStepRunReason.Cached || r == IncrementalStepRunReason.Unchanged, $"step {step} should be cached");
        }
        run.Sources.Should().BeEmpty();
    }

    [TestMethod]
    public void Baseline_generates_all_outputs()
    {
        var (_, driver) = Initial(Files);
        var hints = Sources(driver).Keys.ToList();
        hints.Should().HaveCountGreaterThanOrEqualTo(6, string.Join("\n", hints));
    }

    [TestMethod]
    public void Unrelated_edit_keeps_everything_cached() =>
        AssertEverythingCached(Rerun(Files, Edit("Unrelated.cs", t => t.Replace("int X", "int X; public int Y"))));

    [TestMethod]
    public void Comment_edit_in_union_file_keeps_everything_cached() =>
        AssertEverythingCached(Rerun(Files, Edit("Native1.cs", t => t + "// just a comment\n")));

    [TestMethod]
    public void Whitespace_edit_in_union_file_keeps_everything_cached() =>
        AssertEverythingCached(Rerun(Files, Edit("Explicit.cs", t => t.Replace("public union", "\n\n   public union"))));

    [TestMethod]
    public void Semantic_edit_of_one_extended_union_only_modifies_that_union()
    {
        var run = Rerun(Files, Edit("Native1.cs", t => t.Replace("(Cat, Dog)", "(Cat, Dog, Fish)")));

        run.Count(TrackingNames.ExtendedUnionOutput, IncrementalStepRunReason.Modified).Should().Be(1);
        run.Reasons(TrackingNames.UnionOutput).Should().OnlyContain(r => r == IncrementalStepRunReason.Cached || r == IncrementalStepRunReason.Unchanged);
        run.Sources.Keys.Should().ContainSingle().Which.Should().Contain("Pet");
    }

    [TestMethod]
    public void Semantic_edit_of_other_extended_union_only_modifies_that_union()
    {
        var run = Rerun(Files, Edit("Native2.cs", t => t.Replace("(Cat, Dog, Fish)", "(Cat, Fish)")));

        run.Count(TrackingNames.ExtendedUnionOutput, IncrementalStepRunReason.Modified).Should().Be(1);
        run.Sources.Keys.Should().ContainSingle().Which.Should().Contain("Animal");
    }

    [TestMethod]
    public void Semantic_edit_of_explicit_union_only_modifies_that_union()
    {
        var run = Rerun(Files, Edit("Explicit.cs", t => t.Replace("(Cat, Dog)", "(Cat, Fish)")));

        run.Count(TrackingNames.UnionOutput, IncrementalStepRunReason.Modified).Should().Be(1);
        run.Reasons(TrackingNames.ExtendedUnionOutput).Should().OnlyContain(r => r == IncrementalStepRunReason.Cached || r == IncrementalStepRunReason.Unchanged);
        run.Sources.Keys.Should().ContainSingle().Which.Should().Contain("Explicit");
    }

    [TestMethod]
    public void Semantic_edit_of_union_with_both_attributes_only_modifies_that_union()
    {
        var run = Rerun(Files, Edit("Both.cs", t => t.Replace("(Cat, Fish)", "(Dog, Fish)")));

        run.Count(TrackingNames.UnionOutput, IncrementalStepRunReason.Modified).Should().Be(1);
        run.Reasons(TrackingNames.ExtendedUnionOutput).Should().OnlyContain(r => r == IncrementalStepRunReason.Cached || r == IncrementalStepRunReason.Unchanged);
        run.Sources.Keys.Should().ContainSingle().Which.Should().Contain("Both");
    }

    [TestMethod]
    public void Edit_of_enum_regenerates_enum_output_only()
    {
        var run = Rerun(Files, Edit("Enum.cs", t => t.Replace("Green", "Green, Blue")));

        run.Count(TrackingNames.EnumOutput, IncrementalStepRunReason.Modified).Should().Be(1);
        run.Sources.Keys.Should().ContainSingle().Which.Should().Contain("Color");
    }

    [TestMethod]
    public void Edit_of_result_type_regenerates_result_output_only()
    {
        var run = Rerun(Files, Edit("Result.cs", t => t.Replace("public abstract", "internal abstract")));

        run.Count(TrackingNames.ResultTypeOutput, IncrementalStepRunReason.Modified).Should().Be(1, run.Describe(TrackingNames.ResultTypeOutput));
        run.Sources.Keys.Should().OnlyContain(k => k.Contains("Res"));
    }

    // The error type is held as a symbol that is only compared by name. These tests check whether
    // output derived from the symbol (e.g. IsValueType) goes stale when the name stays the same.
    [TestMethod]
    public void Changing_error_type_from_class_to_struct_does_not_leave_stale_output()
    {
        // ErrorType is a SymbolWrapper compared by name only. The generator reads IsValueType from it, but that value
        // currently never reaches the output (the template has no extension method placeholder), so the cached
        // output is still correct. Rerun asserts incremental output == from-scratch output; if the generator ever
        // starts to emit symbol-derived facts, this test fails until they are moved into the schema.
        const string merge = "[FunicularSwitch.Generators.MergeError] public static MyError MergeErrors(MyError a, MyError b) => a;";
        var before = Edit("MyError.cs", t => t.Replace("{ }", "{ " + merge + " }"));
        var after = before.Select(f => f.path == "MyError.cs" ? (f.path, f.text.Replace("class", "struct")) : f).ToArray();

        Rerun(before, after).Sources.Should().BeEmpty();
    }
    [TestMethod]
    public void Adding_merge_error_method_updates_result_output()
    {
        var run = Rerun(Files, Edit("MyError.cs", t => t.Replace("{ }",
            "{ [FunicularSwitch.Generators.MergeError] public MyError MergeErrors(MyError other) => other; }")));

        run.Count(TrackingNames.ResultTypeOutput, IncrementalStepRunReason.Modified).Should().Be(1, run.Describe(TrackingNames.ResultTypeOutput));
        run.Sources.Keys.Should().ContainSingle().Which.Should().Contain("Res");
    }
}