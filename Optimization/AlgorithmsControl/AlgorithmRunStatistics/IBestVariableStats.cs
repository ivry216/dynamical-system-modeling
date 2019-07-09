using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IBestVariableStats : IAlgorithmStats
    {
        double[] BestSolution { get; }
    }
}
