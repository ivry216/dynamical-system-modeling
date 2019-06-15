using Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public interface IPopulationValueHistoryMaker : IAlgorithmIterationFollower, IAlgorithmStatsGetter<IIterationValuesHistoryStats>
    {
    }
}
