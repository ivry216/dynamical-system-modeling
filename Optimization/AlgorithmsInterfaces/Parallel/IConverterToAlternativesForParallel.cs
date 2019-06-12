using Optimization.Problem.Parallel;

namespace Optimization.AlgorithmsInterfaces.Parallel
{
    public interface IConverterToAlternativesForParallel<TAlternatives>
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        
    }
}
