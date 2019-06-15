using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IBestAlternativeHistoryStats : IAlgorithmStats
    {
        ICollection<double> BestValues { get; }
        ICollection<double[]> BestVarialbes { get; }
    }
}
