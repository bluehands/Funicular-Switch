using FunicularSwitch.Generators;

namespace FunicularSwitch.Generators.Consumer.Net11;

[UnionType]
public closed record Shape;

public sealed record Circle(double Radius) : Shape;
public sealed record Rectangle(double Width, double Height) : Shape;

[UnionType]
public closed class Animal;

public sealed class Cat : Animal;
public sealed class Dog : Animal;

[UnionType]
public closed record Tree<T>;

public sealed record Leaf<T>(T Value) : Tree<T>;
public sealed record Node<T>(Tree<T> Left, Tree<T> Right) : Tree<T>;

[UnionType]
public closed record Expression;

public closed record Binary(Expression Left, Expression Right) : Expression;
public sealed record Add(Expression Left, Expression Right) : Binary(Left, Right);
public sealed record Multiply(Expression Left, Expression Right) : Binary(Left, Right);
public sealed record Constant(int Value) : Expression;

[ResultType]
public closed partial class Result<T>;

public record Cat2(string Name);
public record Dog2(string Name);

[UnionType]
public union Pet(Cat2, Dog2);

[UnionType]
public union Payload(int, string, int[]);

[UnionType]
public union Scalar(int?, string?, bool);
