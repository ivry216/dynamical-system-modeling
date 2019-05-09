namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IBestVariableAndValueStats : IAlgorithmStats
    {
        double BestValue { get; }
        double[] BestAlternative { get; }
    }
}
