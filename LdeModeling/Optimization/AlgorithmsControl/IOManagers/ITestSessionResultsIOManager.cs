using TestApp.Optimization.AlgorithmsControl.AlgorithmMeta;

namespace TestApp.Optimization.AlgorithmsControl.IOManagers
{
    public interface ITestSessionResultsIOManager<TContainingStats, TStats>
        where TContainingStats : IContainingStats<TStats>
        where TStats : IAlgorithmStats
    {
        // TODO: should the method take the launcher or better its results?
        void SaveStats(TContainingStats launcher);
    }
}
