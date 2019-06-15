namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public interface IMessageToFollowers
    {
        double[] BestAlternative { get; }
        double BestValue { get; }
        double[][] Alternatives { get; }
        double[] AlternativeValues { get; }
    }
}
