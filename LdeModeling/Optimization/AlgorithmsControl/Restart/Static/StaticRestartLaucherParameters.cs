namespace TestApp.Optimization.AlgorithmsControl.Restart.Static
{
    public class StaticRestartLaucherParameters : IOptimizationLauncherParameters
    {
        public int Iterations { get; set; }
        public AlgorithmLauncherType LauncherType => AlgorithmLauncherType.StaticRestart;
    }
}
