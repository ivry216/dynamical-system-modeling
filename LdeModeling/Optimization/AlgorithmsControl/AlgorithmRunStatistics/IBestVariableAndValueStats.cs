namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    interface IBestVariableAndValueStats : IAlgorithmStats
    {
        double BestValue { get; }
        double[] BestSolution { get; }
    }
}