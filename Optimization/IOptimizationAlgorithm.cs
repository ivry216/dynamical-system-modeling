using Optimization.Problem;

namespace Optimization
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
