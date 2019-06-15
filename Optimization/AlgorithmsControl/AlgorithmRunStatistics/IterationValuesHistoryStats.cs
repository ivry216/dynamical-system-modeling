using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public class IterationValuesHistoryStats : IIterationValuesHistoryStats
    {
        public ICollection<double[]> Values { get; private set; }

        public IterationValuesHistoryStats(ICollection<double[]> values)
        {
            Values = values;
        }
    }
}
