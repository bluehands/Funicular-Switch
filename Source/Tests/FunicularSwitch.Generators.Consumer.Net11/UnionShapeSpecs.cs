using FluentAssertions;

namespace FunicularSwitch.Generators.Consumer.Net11;

[TestClass]
public class UnionShapeSpecs
{
    [TestMethod]
    public void DifferentlyNamedTypeParameters()
    {
        Tree2<int> tree = new Leaf2<int>(3);
        tree.Match(l => l.Value, n => 0).Should().Be(3);
    }

    [TestMethod]
    public void HandWrittenUnionWithConstructors()
    {
        HandPet pet = new HandPet(new Dog2("Rex"));
        pet.Match(c => c.Name, d => d.Name).Should().Be("Rex");
        var act = () => default(HandPet).Match(c => 1, d => 2);
        act.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void HandWrittenUnionWithCreateMethods()
    {
        ((CreatePet)"text").Match(c => c.Name, s => s ?? "null").Should().Be("text");
        ((CreatePet)new Cat2("Tom")).Match(c => c.Name, s => "s").Should().Be("Tom");
        default(CreatePet).Match(c => "cat", s => s is null ? "null case" : "x").Should().Be("null case");
    }
}
