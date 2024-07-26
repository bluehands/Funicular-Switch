using System.Collections.Concurrent;

namespace FunicularSwitch.Generators.Common;

static class RunCount
{
    static readonly ConcurrentDictionary<string, int> Counters = new();
    public static int Increase(string key) => Counters.AddOrUpdate(key, _ => 1, (_, i) => i + 1);
}