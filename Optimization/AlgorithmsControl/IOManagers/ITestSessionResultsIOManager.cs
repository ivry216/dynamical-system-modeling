using Optimization.AlgorithmsControl.AlgorithmMeta;

namespace Optimization.AlgorithmsControl.IOManagers
{
    public interface ITestSessionResultsIOManager<TContainingStats, TStats>
        where TContainingStats : IContainingStats<TStats>
        where TStats : IAlgorithmStats
    {
        // TODO: should the method take the launcher or better its results?
        void SaveStats(TContainingStats launcher, string fileName);
    }
}
