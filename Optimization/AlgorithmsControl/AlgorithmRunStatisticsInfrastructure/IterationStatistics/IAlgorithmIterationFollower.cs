namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public interface IAlgorithmIterationFollower
    {
        void Update(IMessageToFollowers message);
        void Refresh();
    }
}
