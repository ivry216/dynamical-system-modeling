using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IAlgorithmRunCompositeStats<TBest, TBestSolution>
    {
        IBestSolutionStats<TBest, TBestSolution> BestSolutionStats { get; }
    }
}
