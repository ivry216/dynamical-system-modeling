using Optimization.AlgorithmsControl.Restart.Conditional.Collectors;
using System.Linq;
using System.Collections.Generic;
using MathCore.Vectors;

namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public class BestValueBasedRestart : RestartMetaBase<BestValueBasedRestartParameters>
    {
        private BestValueRestartCollector bestValueRestartCollector;
        private BestVariableRestartCollector bestVariableRestartCollector;

        private IReadOnlyCollection<double> window;
        private IReadOnlyCollection<double[]> bestSolutionsFound;

        public BestValueBasedRestart()
        {
            bestValueRestartCollector = new BestValueRestartCollector();
            bestVariableRestartCollector = new BestVariableRestartCollector();
        }

        protected override void ActAfterAlgorithmIteration()
        {
            bestValueRestartCollector.AddBest(Algorithm.BestValue);
        }

        protected override void ActBeforeRestart()
        {
            bestValueRestartCollector.Reset();
            bestVariableRestartCollector.AddBestAlternative((double[])Algorithm.BestSolution.Clone());
        }

        protected override void InitializeCollectors()
        {
            bestValueRestartCollector.SetWindowSize(Parameters.WindowSize);

            window = bestValueRestartCollector.BestValues;
            bestSolutionsFound = bestVariableRestartCollector.BestVariables;
        }

        protected override bool IsRestartNeeded()
        {
            if (bestSolutionsFound.Any(s => VectorOperator.CalculateAbsDistance(s, Algorithm.BestSolution) < Parameters.BestSolutionRestartDistance))
            {
                System.Console.WriteLine("Restart!");
                return true;
            }

            if (window.Count == Parameters.WindowSize)
            {
                if (window.Max() - window.Min() < Parameters.Threshold)
                {
                    System.Console.WriteLine("Restart!");
                    return true;
                }
            }
            
            return false;
        }
    }
}
