namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_native_union_type_generator : VerifySourceGenerator
{
    [TestMethod]
    public Task For_native_union()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public record Cat(string Name);
            public record Dog(string Name);

            [UnionType]
            public union Pet(Cat, Dog);
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_native_union_with_builtin_array_and_generic_cases()
    {
        var code =
            /* lang=csharp */
            """
            using System.Collections.Generic;
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            internal union Payload(int, string, int[], List<string>);
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_native_union_with_nullable_cases()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public union Scalar(int?, string?, bool);
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_native_union_with_cases_deriving_from_each_other()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public class Animal;
            public class Dog : Animal;

            [UnionType]
            public union Thing(Animal, Dog, string);
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_generic_native_union()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public record Failure(string Message);

            [UnionType]
            public union Outcome<T>(T, Failure) where T : class;
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_native_union_with_several_nullable_cases_routes_default_to_first_nullable()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public record Cat(string Name);

            [UnionType]
            public union Variant(Cat?, int?, string?, bool);
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_native_union_with_type_parameter_cases_keeps_declared_order()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public union Either<TLeft, TRight>(TLeft, TRight) where TLeft : class where TRight : class;
            """;

        return VerifyCSharp15(code);
    }
}