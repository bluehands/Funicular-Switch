using System;
using System.Collections.Immutable;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace DocSamples
{
    [AttributeUsage(AttributeTargets.Method, AllowMultiple = true)]
    public class RunnerAttribute : Attribute
    {
        public string Region { get; }

        public RunnerAttribute(string region) => Region = region;
    }

    public static class SampleRunner
    {
        static readonly Lazy<ImmutableDictionary<string, Func<Task>>> Runners = new Lazy<ImmutableDictionary<string, Func<Task>>>(FindRunners);

        public static async Task Run(RegionSelection region)
        {
            var runners = Runners.Value;

            async Task Execute(string regionName)
            {
                if (!runners.TryGetValue(regionName, out var action))
                    throw new Exception($"Runner for region {regionName} not found");

                await action();
            }

            await region.Match(async all =>
            {
                foreach (var runner in runners)
                {
                    await Execute(runner.Key);
                }
            }, specific => Execute(specific.RegionName));
        }

        static ImmutableDictionary<string, Func<Task>> FindRunners()
        {
            var runners = typeof(Program)
                .Assembly
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                .SelectMany(m =>
                {
                    var runnerAttributes = m.GetCustomAttributes<RunnerAttribute>();
                    return runnerAttributes.Select(runnerAttribute =>
                    {
                        var run = (Func<Task>)(() =>
                        {
                            if (m.ReturnType == typeof(void))
                            {
                                m.Invoke(null, null);
                                return Task.CompletedTask;
                            }

                            var result = m.Invoke(null, null);
                            return result is Task task ? task : Task.FromResult(result);
                        });
                        return (region: runnerAttribute.Region, action: run);
                    });
                }).ToImmutableDictionary(t => t.region, t => t.action);
            return runners;
        }
    }

    public abstract class RegionSelection
    {
        public static readonly RegionSelection All = new All_();

        public static RegionSelection Specific(string regionName) => new Specific_(regionName);

        public class All_ : RegionSelection
        {
            public All_() : base(UnionCases.All)
            {
            }
        }

        public class Specific_ : RegionSelection
        {
            public string RegionName { get; }

            public Specific_(string regionName) : base(UnionCases.Specific) => RegionName = regionName;
        }

        internal enum UnionCases
        {
            All,
            Specific
        }

        internal UnionCases UnionCase { get; }
        RegionSelection(UnionCases unionCase) => UnionCase = unionCase;

        public override string ToString() => Enum.GetName(typeof(UnionCases), UnionCase) ?? UnionCase.ToString();
        bool Equals(RegionSelection other) => UnionCase == other.UnionCase;

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != GetType()) return false;
            return Equals((RegionSelection)obj);
        }

        public override int GetHashCode() => (int)UnionCase;
    }

    static class RegionSelectionExtension
    {
        public static T Match<T>(this RegionSelection regionSelection, Func<RegionSelection.All_, T> all,
            Func<RegionSelection.Specific_, T> specific)
        {
            switch (regionSelection.UnionCase)
            {
                case RegionSelection.UnionCases.All:
                    return all((RegionSelection.All_)regionSelection);
                case RegionSelection.UnionCases.Specific:
                    return specific((RegionSelection.Specific_)regionSelection);
                default:
                    throw new ArgumentException(
                        $"Unknown type implementing RegionSelection: {regionSelection.GetType().Name}");
            }
        }

        public static async Task<T> Match<T>(this RegionSelection regionSelection,
            Func<RegionSelection.All_, Task<T>> all, Func<RegionSelection.Specific_, Task<T>> specific)
        {
            switch (regionSelection.UnionCase)
            {
                case RegionSelection.UnionCases.All:
                    return await all((RegionSelection.All_)regionSelection).ConfigureAwait(false);
                case RegionSelection.UnionCases.Specific:
                    return await specific((RegionSelection.Specific_)regionSelection).ConfigureAwait(false);
                default:
                    throw new ArgumentException(
                        $"Unknown type implementing RegionSelection: {regionSelection.GetType().Name}");
            }
        }

        public static async Task<T> Match<T>(this Task<RegionSelection> regionSelection,
            Func<RegionSelection.All_, T> all, Func<RegionSelection.Specific_, T> specific)
        {
            return (await regionSelection.ConfigureAwait(false)).Match(all, specific);
        }

        public static async Task<T> Match<T>(this Task<RegionSelection> regionSelection,
            Func<RegionSelection.All_, Task<T>> all, Func<RegionSelection.Specific_, Task<T>> specific)
        {
            return await (await regionSelection.ConfigureAwait(false)).Match(all, specific).ConfigureAwait(false);
        }
    }
}