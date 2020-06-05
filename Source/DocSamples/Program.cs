using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using FunicularSwitch;

namespace DocSamples
{
    class Program
    {
        ///<param name="region">Takes in the --region option from the code fence options in markdown</param>
        ///<param name="session">Takes in the --session option from the code fence options in markdown</param>
        ///<param name="package">Takes in the --package option from the code fence options in markdown</param>
        ///<param name="project">Takes in the --project option from the code fence options in markdown</param>
        ///<param name="args">Takes in any additional arguments passed in the code fence options in markdown</param>
        ///<see>To learn more see <a href="https://aka.ms/learntdn">our documentation</a></see>
        static async Task<int> Main(
            string region = null,
            string session = null,
            string package = null,
            string project = null,
            string[] args = null)
        {
            var regionsSelection = region switch
            {
                null => RegionSelection.All,
                _ => RegionSelection.Specific(region)
            };

            return await Run(() => Run(regionsSelection, session));
        }

        static async Task Run(RegionSelection region, string session)
        {
            var runners = FindRunners();

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

        static Dictionary<string, Func<Task>> FindRunners()
        {
            var runners = typeof(Program)
                .Assembly
                .GetTypes()
                .SelectMany(t => t.GetMethods(BindingFlags.Static | BindingFlags.NonPublic | BindingFlags.Public))
                .Choose(m =>
                {
                    var runnerAttribute = m.GetCustomAttribute<RunnerAttribute>();
                    if (runnerAttribute != null)
                        return (runnerAttribute.Region, (Func<Task>) (() =>
                        {
                            if (m.ReturnType == typeof(void))
                            {
                                m.Invoke(null, null);
                                return Task.CompletedTask;
                            }

                            return (Task) m.Invoke(null, null);
                        }));
                    return Option<(string region, Func<Task> action)>.None;
                }).ToDictionary(t => t.region, t => t.action);
            return runners;
        }

        static async Task<int> Run(Func<Task> sample)
        {
            try
            {
                await sample();
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Execution failed: " + e);
                return 1;
            }
        }
    }

    abstract class RegionSelection
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
        public static T Match<T>(this RegionSelection regionSelection, Func<RegionSelection.All_, T> all, Func<RegionSelection.Specific_, T> specific)
        {
            switch (regionSelection.UnionCase)
            {
                case RegionSelection.UnionCases.All:
                    return all((RegionSelection.All_)regionSelection);
                case RegionSelection.UnionCases.Specific:
                    return specific((RegionSelection.Specific_)regionSelection);
                default:
                    throw new ArgumentException($"Unknown type implementing RegionSelection: {regionSelection.GetType().Name}");
            }
        }

        public static async Task<T> Match<T>(this RegionSelection regionSelection, Func<RegionSelection.All_, Task<T>> all, Func<RegionSelection.Specific_, Task<T>> specific)
        {
            switch (regionSelection.UnionCase)
            {
                case RegionSelection.UnionCases.All:
                    return await all((RegionSelection.All_)regionSelection).ConfigureAwait(false);
                case RegionSelection.UnionCases.Specific:
                    return await specific((RegionSelection.Specific_)regionSelection).ConfigureAwait(false);
                default:
                    throw new ArgumentException($"Unknown type implementing RegionSelection: {regionSelection.GetType().Name}");
            }
        }

        public static async Task<T> Match<T>(this Task<RegionSelection> regionSelection, Func<RegionSelection.All_, T> all, Func<RegionSelection.Specific_, T> specific)
        {
            return (await regionSelection.ConfigureAwait(false)).Match(all, specific);
        }

        public static async Task<T> Match<T>(this Task<RegionSelection> regionSelection, Func<RegionSelection.All_, Task<T>> all, Func<RegionSelection.Specific_, Task<T>> specific)
        {
            return await(await regionSelection.ConfigureAwait(false)).Match(all, specific).ConfigureAwait(false);
        }
    }
}
