using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.AlgorithmsControl.InformationCollectingManager;

namespace TestApp.Optimization.AlgorithmsControl.Restart.Static
{
    public class StaticRestartLauncher : OptimizationLauncher<StaticRestartLaucherParameters, RealAlgorithmDataCollector>
    {
        public override AlgorithmLauncherType LauncherType => AlgorithmLauncherType.StaticRestart;

        public StaticRestartLauncher(StaticRestartLaucherParameters parameters) : base(parameters)
        { }

        public override void Run()
        {
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                // Evaluate algorithm
                Algorithm.Evaluate();
                // Save the results to collector
                collector.AddBest(Algorithm.BestValue);
                collector.AddBestSolution(Algorithm.BestSolution);
            }
        }
    }
}
