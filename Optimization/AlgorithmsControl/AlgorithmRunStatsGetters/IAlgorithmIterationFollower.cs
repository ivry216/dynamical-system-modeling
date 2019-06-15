namespace Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    interface IAlgorithmIterationFollower
    {
        void Update(IMessageToFollowers message);
    }
}
