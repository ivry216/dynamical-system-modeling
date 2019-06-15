using Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public interface IIterationValueHistoryMaker : IAlgorithmIterationFollower, IAlgorithmStatsGetter<IIterationValuesHistoryStats>
    {
    }
}
