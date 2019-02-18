using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace TestApp.Optimization.AlgorithmsControl
{
    public interface IOptimizationLauncherResult<TBest, TBestSolution, TRunStats>
        where TRunStats : IAlgorithmRunCompositeStats<TBest, TBestSolution>
    {
        int Count { get; }
        TRunStats[] Stats { get; }
    }
}
