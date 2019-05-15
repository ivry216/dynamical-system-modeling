using System;
using TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using TestApp.Optimization.AlgorithmsControl.InformationCollectingManager;

namespace TestApp.Optimization.AlgorithmsControl.Restart.Static
{
    public class StaticRestartLauncher : OptimizationLauncher<StaticRestartLaucherParameters, RealAlgorithmDataCollector, IBestVariableAndValueStats>
    {
        public StaticRestartLauncher(StaticRestartLaucherParameters parameters) : base(parameters)
        { }

        public override void Run()
        {
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                Console.WriteLine($"Test number: {i}");
                // Evaluate algorithm
                Algorithm.Evaluate();
                // Save the results to collector
                collector.Add(new BestVariableAndValueStats(Algorithm.BestValue, Algorithm.BestSolution));
            }
        }
    }
}
