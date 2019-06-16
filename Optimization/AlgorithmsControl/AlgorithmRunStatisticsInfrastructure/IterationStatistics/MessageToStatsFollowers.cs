namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public class MessageToStatsFollowers : IMessageToFollowers
    {
        public double[] BestAlternative { get; }

        public double BestValue { get; }

        public double[][] Alternatives { get; }

        public double[] AlternativeValues { get; }

        public MessageToStatsFollowers(double[] bestAlternative, double bestValue, double[][] alternatives, double[] alternativeValues)
        {
            BestAlternative = bestAlternative;
            BestValue = bestValue;
            Alternatives = alternatives;
            AlternativeValues = alternativeValues;
        }
    }
}
