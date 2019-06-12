using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IBestVariableAndValueStats : IAlgorithmStats
    {
        double BestValue { get; }
        double[] BestSolution { get; }
    }
}