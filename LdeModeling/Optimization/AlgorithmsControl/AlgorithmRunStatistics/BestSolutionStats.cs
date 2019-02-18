using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public abstract class BestSolutionStats<TBest, TBestSolution> : IBestSolutionStats<TBest, TBestSolution>
    {
        public TBest BestValue { get; protected set; }

        public TBestSolution BestSolution { get; protected set; }

        object IBestSolutionStats.BestValue => BestValue;

        object IBestSolutionStats.BestSolution => BestSolution;

        public BestSolutionStats(TBest best, TBestSolution bestSolution)
        {
            Initialize(best, bestSolution);
        }

        protected abstract void Initialize(TBest best, TBestSolution bestSolution);
    }
}
