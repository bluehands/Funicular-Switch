using System.Collections.Generic;

// ReSharper disable once CheckNamespace
;

public delegate IEnumerable<TError> Validate<in T, out TError>(T item);

public class Unit
{
    public static readonly Unit Instance = new Unit();

    Unit()
    {
    }
}

public static class No
{
    public static Unit Thing => Unit.Instance;
}
