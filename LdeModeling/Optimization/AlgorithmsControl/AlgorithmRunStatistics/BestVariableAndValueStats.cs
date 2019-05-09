using TestApp.MathematicalCore.ArrayExtensions;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public class BestVariableAndValueStats : IBestVariableAndValueStats
    {
        public double BestValue { get; protected set; }
        public double[] BestAlternative { get; protected set; }

        public BestVariableAndValueStats(double value, double[] variable)
        {
            BestValue = value;
            BestAlternative = variable.CopyVector();
        }
    }
}
