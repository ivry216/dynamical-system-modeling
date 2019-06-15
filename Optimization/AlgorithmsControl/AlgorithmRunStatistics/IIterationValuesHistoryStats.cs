using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    interface IIterationValuesHistoryStats : IAlgorithmStats
    {
        ICollection<double[]> Values { get; }
    }
}
