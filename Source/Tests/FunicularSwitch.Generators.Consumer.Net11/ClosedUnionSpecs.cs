using FluentAssertions;

namespace FunicularSwitch.Generators.Consumer.Net11;

[TestClass]
public class ClosedUnionSpecs
{
    //native exhaustive switches: compile without CS8509 (TreatWarningsAsErrors), no default arm needed
    static double Area(Shape shape) => shape switch
    {
        Circle c => Math.PI * c.Radius * c.Radius,
        Rectangle r => r.Width * r.Height,
    };

    static int Evaluate(Expression e) => e switch
    {
        Add a => Evaluate(a.Left) + Evaluate(a.Right),
        Multiply m => Evaluate(m.Left) * Evaluate(m.Right),
        Constant c => c.Value,
    };

    [TestMethod]
    public void NativeSwitchAndMatchAgree()
    {
        Shape shape = new Rectangle(2, 3);
        Area(shape).Should().Be(6);
        shape.Match(c => c.Radius, r => r.Width).Should().Be(2);
    }

    [TestMethod]
    public void MultiLevelClosedHierarchy()
    {
        Expression e = new Add(new Constant(2), new Multiply(new Constant(3), new Constant(4)));
        Evaluate(e).Should().Be(14);
        e.Match(add => "add", constant => "constant", multiply => "multiply").Should().Be("add");
    }

    [TestMethod]
    public void ClosedClassAndGenericUnions()
    {
        Animal animal = new Dog();
        animal.Match(_ => "cat", _ => "dog").Should().Be("dog");

        Tree<int> tree = new Node<int>(new Leaf<int>(1), new Leaf<int>(2));
        tree.Match(leaf => leaf.Value, node => 0).Should().Be(0);
    }

    [TestMethod]
    public void SwitchCallsMatchingHandler()
    {
        var called = "";
        ((Shape)new Circle(1)).Switch(_ => called = "circle", _ => called = "rectangle");
        called.Should().Be("circle");
    }

    [TestMethod]
    public async Task AsyncMatch()
    {
        Shape shape = new Circle(1);
        (await shape.Match(c => Task.FromResult(1), r => Task.FromResult(2))).Should().Be(1);
        (await Task.FromResult(shape).Match(c => 1, r => 2)).Should().Be(1);
    }

    [TestMethod]
    public void NullReceiverThrowsArgumentNullException()
    {
        Shape shape = null!;
        var act = () => shape.Match(_ => 1, _ => 2);
        act.Should().Throw<ArgumentNullException>().WithParameterName("shape");
        //still an ArgumentException for existing catch blocks
        act.Should().Throw<ArgumentException>();
        var switchAct = () => shape.Switch(_ => { }, _ => { });
        switchAct.Should().Throw<ArgumentNullException>();
    }

    [TestMethod]
    public void ClosedResultType()
    {
        Result<int> result = Result.Ok(42);
        var text = result switch
        {
            Result<int>.Ok_ ok => $"ok {ok.Value}",
            Result<int>.Error_ error => $"error {error.Details}",
        };
        text.Should().Be("ok 42");
        result.Match(i => i, _ => -1).Should().Be(42);
        Result.Error<int>("failed").Match(i => i, _ => -1).Should().Be(-1);
    }
}
