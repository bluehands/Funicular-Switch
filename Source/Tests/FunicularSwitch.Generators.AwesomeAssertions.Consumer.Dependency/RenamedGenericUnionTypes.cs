namespace FunicularSwitch.Generators.AwesomeAssertions.Consumer.Dependency;

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial record ExternalGenericUnion<T>;

public sealed record ExternalValue<TValue>(TValue Value) : ExternalGenericUnion<TValue>;

public sealed record ExternalEmpty<TItem> : ExternalGenericUnion<TItem>;

[UnionType(CaseOrder = CaseOrder.AsDeclared)]
public abstract partial record ReorderedGenericUnion<TFirst, TSecond>;

public sealed record ReorderedValue<TValue, TKey>(TKey Key, TValue Value) : ReorderedGenericUnion<TKey, TValue>;
