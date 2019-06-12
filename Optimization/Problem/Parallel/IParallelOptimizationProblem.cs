namespace Optimization.Problem.Parallel
{
    public interface IParallelOptimizationProblem<TValues, TAlternatives>
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        int Dimension { get; }
        bool IsConstrained { get; }

        TValues CalculateCriterion(TAlternatives alternatives);
    }
}
