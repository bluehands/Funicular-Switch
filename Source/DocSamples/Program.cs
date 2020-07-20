using System;
using System.Threading.Tasks;

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

            return await Run(() => SampleRunner.Run(regionsSelection, session));
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
}
