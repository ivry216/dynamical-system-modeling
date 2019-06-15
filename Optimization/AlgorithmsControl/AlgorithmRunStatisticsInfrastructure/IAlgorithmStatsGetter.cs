using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure
{
    public interface IAlgorithmStatsGetter<TStats>
        where TStats : IAlgorithmStats
    {
        TStats GetStats();
    }
}
