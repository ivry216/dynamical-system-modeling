using Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public interface IBestAlternativeHistoryMaker : IAlgorithmIterationFollower, IAlgorithmStatsGetter<IBestAlternativeHistoryStats>
    {
    }
}
