using MathCore.Extensions.Arrays;
using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public class BestAlternativeHistoryMaker : IBestAlternativeHistoryMaker
    {
        private List<double[]> bestAlternatives;
        private List<double> bestValues;

        public AlgorithmStatsFollowerType Type => AlgorithmStatsFollowerType.BestAlternativeHistory;

        public BestAlternativeHistoryMaker()
        {
            bestAlternatives = new List<double[]>();
            bestValues = new List<double>();
        }

        public IBestAlternativeHistoryStats GetStats()
        {
            return new BestAlternativeHistoryStats(bestValues, bestAlternatives);
        }

        public void Refresh()
        {
            bestAlternatives.Clear();
            bestValues.Clear();
        }

        public void Update(IMessageToFollowers message)
        {
            bestAlternatives.Add(message.BestAlternative.CopyVector());
            bestValues.Add(message.BestValue);
        }
    }
}
