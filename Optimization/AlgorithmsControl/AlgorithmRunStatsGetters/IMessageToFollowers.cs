namespace Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    interface IMessageToFollowers
    {
        double[] BestAlternative { get; }
        double BestValue { get; }
        double[][] Alternatives { get; }
        double[] AlternativeValues { get; }
    }
}
