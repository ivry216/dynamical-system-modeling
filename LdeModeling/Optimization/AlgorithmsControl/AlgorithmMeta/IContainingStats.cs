using System.Collections.Generic;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmMeta
{
    public interface IContainingStats<TStats>
        where TStats : IAlgorithmStats
    {
        List<TStats> Stats { get; }
    }
}
