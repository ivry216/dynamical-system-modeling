using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmMeta
{
    public interface IContainingStats<TStats>
        where TStats : IAlgorithmStats
    {
        List<TStats> Stats { get; }
    }
}
