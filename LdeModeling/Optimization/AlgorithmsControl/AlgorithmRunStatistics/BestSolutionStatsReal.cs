using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.MathematicalCore.ArrayExtensions;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public class BestSolutionStatsReal : BestSolutionStats<double, double[]>
    {
        public BestSolutionStatsReal(double best, double[] bestSolution) : base(best, bestSolution)
        { }

        protected override void Initialize(double best, double[] bestSolution)
        {
            BestValue = best;
            BestSolution = new double[bestSolution.Length];
            BestSolution.FillWithVector( bestSolution);
        }
    }
}
