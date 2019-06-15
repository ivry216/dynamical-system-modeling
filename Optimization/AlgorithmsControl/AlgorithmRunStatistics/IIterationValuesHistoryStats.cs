using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IIterationValuesHistoryStats : IAlgorithmStats
    {
        ICollection<double[]> Values { get; }
    }
}
