using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IBestSolutionStats
    {
        object BestValue { get; }
        object BestSolution { get; }
    }

    public interface IBestSolutionStats<TBest, TBestSolution> : IBestSolutionStats
    {
        new TBest BestValue { get; }
        new TBestSolution BestSolution { get; }
    }
}