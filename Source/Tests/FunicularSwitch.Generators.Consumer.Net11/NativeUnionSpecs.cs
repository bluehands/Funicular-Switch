using FluentAssertions;

namespace FunicularSwitch.Generators.Consumer.Net11;

[TestClass]
public class NativeUnionSpecs
{
    [TestMethod]
    public void MatchHandlesEachCase()
    {
        Pet pet = new Cat2("Tom");
        pet.Match(cat => $"cat {cat.Name}", dog => $"dog {dog.Name}").Should().Be("cat Tom");

        pet = new Dog2("Rex");
        pet.Match(cat => $"cat {cat.Name}", dog => $"dog {dog.Name}").Should().Be("dog Rex");
    }

    [TestMethod]
    public void BuiltInAndArrayCases()
    {
        Payload payload = 42;
        payload.Match(i => $"int {i}", s => $"string {s}", a => $"array {a.Length}").Should().Be("int 42");
        payload = new[] { 1, 2, 3 };
        payload.Match(i => $"int {i}", s => $"string {s}", a => $"array {a.Length}").Should().Be("array 3");
        payload = "x";
        payload.Match(i => $"int {i}", s => $"string {s}", a => $"array {a.Length}").Should().Be("string x");
    }

    [TestMethod]
    public void SwitchCallsMatchingHandler()
    {
        Payload payload = "text";
        var seen = "";
        payload.Switch(i => seen = "int", s => seen = s, a => seen = "array");
        seen.Should().Be("text");
    }

    [TestMethod]
    public async Task AsyncMatchAndTaskExtensions()
    {
        Pet pet = new Dog2("Rex");
        (await pet.Match(c => Task.FromResult(c.Name), d => Task.FromResult(d.Name))).Should().Be("Rex");
        (await Task.FromResult(pet).Match(c => c.Name, d => d.Name)).Should().Be("Rex");

        var seen = "";
        await pet.Switch(c => { seen = c.Name; return Task.CompletedTask; }, d => { seen = d.Name; return Task.CompletedTask; });
        seen.Should().Be("Rex");
    }

    [TestMethod]
    public void DefaultUnionWithoutNullableCaseThrowsInvalidOperation()
    {
        Pet pet = default;
        var act = () => pet.Match(_ => 1, _ => 2);
        act.Should().Throw<InvalidOperationException>().WithMessage("Union has no value");
        var switchAct = () => pet.Switch(_ => { }, _ => { });
        switchAct.Should().Throw<InvalidOperationException>();
    }

    [TestMethod]
    public void NullIsRoutedToFirstNullableCase()
    {
        Scalar scalar = default;
        scalar.Match(i => $"int {i.HasValue}", s => $"string {s is null}", b => "bool").Should().Be("int False");

        scalar = "text";
        scalar.Match(i => "int", s => $"string {s}", b => "bool").Should().Be("string text");

        scalar = 5;
        scalar.Match(i => $"int {i}", s => "string", b => "bool").Should().Be("int 5");

        scalar = true;
        scalar.Match(i => "int", s => "string", b => "bool").Should().Be("bool");
    }

    [TestMethod]
    public void ExtendUnionsAttributeGeneratesForAllUnions()
    {
        Vehicle v = 3;
        v.Match(c => "cat", d => "dog", i => $"int {i}").Should().Be("int 3");

        Secret s = "x";
        s.Match(str => str, i => i.ToString()).Should().Be("x");
    }

    [TestMethod]
    public void UnionWithUnionTypeAndExtendUnionsIsGeneratedOnce()
    {
        //would not compile with duplicate PetMatchExtension
        Pet pet = new Cat2("Tom");
        pet.Match(c => c.Name, d => d.Name).Should().Be("Tom");
    }

    [TestMethod]
    public void HandWrittenUnionStructIsCoveredByExtendUnions()
    {
        typeof(Vehicle).Assembly.GetType("FunicularSwitch.Generators.Consumer.Net11.HandPlainMatchExtension").Should().NotBeNull();
    }
}
