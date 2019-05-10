using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.Optimization.AlgorithmsControl.AlgorithmStates;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public class BestVariableAndValueStats : IBestVariableAndValueState
    {
        public double BestValue { get; protected set; }
        public double[] BestSolution { get; protected set; }

        public BestVariableAndValueStats(double value, double[] variable)
        {
            BestValue = value;
            BestSolution = variable.CopyVector();
        }
    }
}
