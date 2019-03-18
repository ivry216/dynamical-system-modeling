using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.Problem;

namespace TestApp.Optimization
{
    public interface IOptimizationAlgorithm
    {
        void Evaluate();

        double[] BestSolution { get; }
        double BestValue { get; }
    }

    public interface IOptimizationAlgorithm<TAlgorithmParametesr> : IOptimizationAlgorithm
        where TAlgorithmParametesr : OptimizationAlgorithmParameters
    {
        void SetParameters(TAlgorithmParametesr parameters);
        void SetProblem(OptimizationProblem problem);
    }
}
