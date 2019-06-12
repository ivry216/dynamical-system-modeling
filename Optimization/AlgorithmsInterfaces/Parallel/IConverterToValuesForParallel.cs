using Optimization.Problem.Parallel;

namespace Optimization.AlgorithmsInterfaces.Parallel
{
    public interface IConverterToValuesForParallel<TValues>
        where TValues : IParallelOptimizationProblemValues
    {

    }
}
