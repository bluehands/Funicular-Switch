namespace FunicularSwitch.Generators.Test;

[TestClass]
public class Run_union_type_generator_for_more_shapes : VerifySourceGenerator
{
    [TestMethod]
    public Task For_closed_generic_union_type_with_differently_named_case_type_parameters()
    {
        var code =
            /* lang=csharp */
            """
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            [UnionType]
            public closed record Tree<T>;

            public sealed record Leaf<U>(U Value) : Tree<U>;
            public sealed record Node<V>(Tree<V> Left, Tree<V> Right) : Tree<V>;
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_hand_written_union_struct()
    {
        var code =
            /* lang=csharp */
            """
            using System.Runtime.CompilerServices;
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public record Cat(string Name);
            public record Dog(string Name);

            [Union, UnionType]
            public readonly struct Pet : IUnion
            {
                public Pet(Cat value) => Value = value;
                public Pet(Dog value) => Value = value;
                public object? Value { get; }
            }
            """;

        return VerifyCSharp15(code);
    }

    [TestMethod]
    public Task For_hand_written_union_struct_with_create_methods()
    {
        var code =
            /* lang=csharp */
            """
            using System.Runtime.CompilerServices;
            using FunicularSwitch.Generators;

            namespace FunicularSwitch.Test;

            public record Cat(string Name);

            [Union, UnionType]
            public readonly struct Pet : IUnion, Pet.IUnionMembers
            {
                public interface IUnionMembers
                {
                    static Pet Create(Cat value) => new(value);
                    static Pet Create(string? value) => new(value);
                    object? Value { get; }
                }
                Pet(object? value) => Value = value;
                public object? Value { get; }
                object? IUnionMembers.Value => Value;
            }
            """;

        return VerifyCSharp15(code);
    }}
