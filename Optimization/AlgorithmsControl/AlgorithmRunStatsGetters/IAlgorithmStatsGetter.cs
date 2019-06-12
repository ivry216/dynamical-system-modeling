namespace Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    public interface IAlgorithmStatsGetter<TStats>
        where TStats : IAlgorithmState
    {
        TStats GetStats();
    }
}
