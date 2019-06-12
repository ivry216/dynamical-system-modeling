using Optimization.Problem.Parallel;

namespace Optimization.AlgorithmsInterfaces.Parallel
{
    public interface IConverterToAlternativesForParallel<TAlternatives, TAlternativeRepresentations>
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        TAlternatives GetAlternatives(TAlternativeRepresentations representations);
    }
}
