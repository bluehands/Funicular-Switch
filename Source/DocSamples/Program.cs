using System;
using System.Threading.Tasks;

namespace DocSamples
{
    class Program
    {
        // Usage: DocSamples [sampleName]. Without an argument all samples are run.
        static async Task<int> Main(string[] args)
        {
            var regionsSelection = args.Length == 0
                ? RegionSelection.All
                : RegionSelection.Specific(args[0]);

            try
            {
                await SampleRunner.Run(regionsSelection);
                return 0;
            }
            catch (Exception e)
            {
                Console.WriteLine("Execution failed: " + e);
                return 1;
            }
        }
    }
}
