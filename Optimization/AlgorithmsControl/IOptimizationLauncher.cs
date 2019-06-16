using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl
{
    public interface IOptimizationLauncher<TParameters, TStats, TAlgorithm>
        where TParameters : IOptimizationLauncherParameters
        where TStats : IAlgorithmStats
        where TAlgorithm : IRealAlgorithm
    {
        List<TStats> Stats { get; }

        TAlgorithm Algorithm { get; set; }
        TParameters Parameters { get; }

        void Run();
    }
}