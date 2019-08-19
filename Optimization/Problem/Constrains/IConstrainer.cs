using Optimization.Problem.Parallel;

namespace Optimization.Problem.Constrains
{
    public interface IConstrainer<TValues, TAlternatives>
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        TValues Evaluate(TAlternatives alternatives);
    }
}
