using System;

namespace DocSamples
{
    class RunnerAttribute : Attribute
    {
        public string Region { get; }

        public RunnerAttribute(string region) => Region = region;
    }
}