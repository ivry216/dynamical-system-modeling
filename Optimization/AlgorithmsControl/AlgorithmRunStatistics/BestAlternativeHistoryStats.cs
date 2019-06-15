using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public class BestAlternativeHistoryStats : IBestAlternativeHistoryStats
    {
        public ICollection<double> BestValues { get; private set; }

        public ICollection<double[]> BestVarialbes { get; private set; }

        public BestAlternativeHistoryStats(ICollection<double> values, ICollection<double[]> bestAlternatives)
        {
            BestValues = values;
            BestVarialbes = bestAlternatives;
        }
    }
}
