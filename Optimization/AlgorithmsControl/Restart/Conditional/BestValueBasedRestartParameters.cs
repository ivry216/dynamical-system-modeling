namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public class BestValueBasedRestartParameters : IOptimizationLauncherParameters
    {
        public AlgorithmLauncherType LauncherType => AlgorithmLauncherType.BestValueBased;
    }
}
