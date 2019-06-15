using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    interface IBestAlternativeHistoryStats : IAlgorithmStats
    {
        ICollection<double> BestValues { get; }
        ICollection<double[]> BestVarialbes { get; }
    }
}
