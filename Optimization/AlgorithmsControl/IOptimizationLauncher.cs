using System.Collections.Generic;

namespace Optimization.AlgorithmsControl
{
    public interface IOptimizationLauncher<TParameters, TStats>
        where TParameters : IOptimizationLauncherParameters
        where TStats : IAlgorithmStats
    {
        List<TStats> Stats { get; }

        IRealAlgorithm Algorithm { get; set; }
        TParameters Parameters { get; }

        void Run();
    }
}