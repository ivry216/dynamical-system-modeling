namespace TestApp.Optimization.AlgorithmsControl
{
    public interface IOptimizationLauncher<TParameters>
        where TParameters : IOptimizationLauncherParameters
    {
        IRealAlgorithm Algorithm { get; set; }
        TParameters Parameters { get; }

        void Run();
    }
}
