namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_closed_union_type_generator : VerifySourceGenerator
{
    [TestMethod]
    public Task For_closed_record_union_type()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public closed record Base;

            public sealed record One : Base;
            public sealed record Aaa : Base;
            public sealed record Two : Base;
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_closed_class_union_type()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public closed class Base;

            public sealed class One : Base;
            public sealed class Two : Base;
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_closed_generic_union_type()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public closed record Tree<T>;

            public sealed record Leaf<T>(T Value) : Tree<T>;
            public sealed record Node<T>(Tree<T> Left, Tree<T> Right) : Tree<T>;
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_closed_multi_level_union_type()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public closed record Base;

            public closed record Mid : Base;
            public sealed record MidA : Mid;
            public sealed record MidB : Mid;
            public sealed record Other : Base;
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_closed_union_type_with_non_closed_abstract_intermediate_keeps_default_arm()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public closed record Base;

            public abstract record Mid : Base;
            public sealed record MidA : Mid;
            public sealed record Other : Base;
            """;

        // compiler cannot prove exhaustiveness here, so the generator keeps the default arm
        return Verify(code, languageVersion: Microsoft.CodeAnalysis.CSharp.LanguageVersion.CSharp15, useNet11References: true);
    }

    [TestMethod]
    public Task For_abstract_record_union_type_under_csharp15_keeps_default_arm()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public abstract record Base;

            public sealed record One : Base;
            public sealed record Two : Base;
            """;

        return VerifyCSharp15(code);
    }
}
