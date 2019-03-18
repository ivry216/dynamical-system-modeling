using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IAlgorithmRunCompositeStats<TBestSolutionStats, TBest, TBestSolution>
        where TBestSolutionStats : IBestSolutionStats<TBest, TBestSolution>
    {
        IBestSolutionStats<TBest, TBestSolution> BestSolutionStats { get; }
    }
}
