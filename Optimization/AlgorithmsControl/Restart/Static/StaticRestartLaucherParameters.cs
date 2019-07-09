namespace Optimization.AlgorithmsControl.Restart.Static
{
    public class StaticRestartLaucherParameters : IOptimizationLauncherParameters
    {
        public AlgorithmLauncherType LauncherType => AlgorithmLauncherType.StaticRestart;

        public int Iterations { get; set; }
    }
}