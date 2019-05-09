namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    public interface IAlgorithmStatsGetter<TStats>
        where TStats : IAlgorithmStats
    {
        TStats GetStats();
    }
}
