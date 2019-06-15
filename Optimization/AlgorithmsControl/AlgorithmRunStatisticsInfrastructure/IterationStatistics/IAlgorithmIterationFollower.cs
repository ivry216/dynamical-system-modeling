namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public interface IAlgorithmIterationFollower
    {
        AlgorithmStatsFollowerType Type { get; }

        void Update(IMessageToFollowers message);
        void Refresh();
    }
}
