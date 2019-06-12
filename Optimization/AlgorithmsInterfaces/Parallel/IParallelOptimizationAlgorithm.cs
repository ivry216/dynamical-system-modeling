using Optimization.Problem;
using Optimization.Problem.Parallel;

namespace Optimization.AlgorithmsInterfaces
{
    public interface IParallelOptimizationAlgorithm : IOptimizationAlgorithm
    {
    }

    public interface IParallelOptimizationAlgorithm<TAlgorithmParametesr, TParallelProblem, TValues, TAlternatives> : IParallelOptimizationAlgorithm
        where TAlgorithmParametesr : OptimizationAlgorithmParameters
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
        where TParallelProblem : ParallelOptimizationProblem<TValues, TAlternatives>
    {
        void SetParameters(TAlgorithmParametesr parameters);
        void SetProblem(TParallelProblem problem);
    }
}
