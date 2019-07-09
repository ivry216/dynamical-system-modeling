namespace Optimization.AlgorithmsControl
{
    public interface IOptimizationLauncherParameters
    {
        int Iterations { get; set; }
        AlgorithmLauncherType LauncherType { get; }
    }
}
