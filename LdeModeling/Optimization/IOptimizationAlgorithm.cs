using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.Problem;

namespace TestApp.Optimization
{
    interface IOptimizationAlgorithm<AlgorithmParameters>
        where AlgorithmParameters : OptimizationAlgorithmParameters
    {
        void SetParameters(AlgorithmParameters parameters);
        void SetProblem(OptimizationProblem problem);
        void Evaluate();

        double[] BestSolution { get; }
        double BestValue { get; }
    }
}
