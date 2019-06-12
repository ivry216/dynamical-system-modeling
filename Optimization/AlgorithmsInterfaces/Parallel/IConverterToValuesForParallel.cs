using Optimization.Problem.Parallel;

namespace Optimization.AlgorithmsInterfaces.Parallel
{
    public interface IConverterToValuesForParallel<TValues, TCalculationResult>
        where TValues : IParallelOptimizationProblemValues
    {
        TCalculationResult GetValues(TValues calculationResult);
    }
}
