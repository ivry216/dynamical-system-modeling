namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatsMiner
{
    public interface IAlgorithmStatsGetter<TStats>
        where TStats : IAlgorithmStats
    {
        TStats GetStats();
    }
}
