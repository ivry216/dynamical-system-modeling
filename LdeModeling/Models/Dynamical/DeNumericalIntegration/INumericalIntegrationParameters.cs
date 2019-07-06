namespace TestApp.Models.Dynamical.DeNumericalIntegration
{
    public interface INumericalIntegrationParameters
    {
        double FinalTime { get; }
        double StartTime { get; }
        int Steps { get; }
        double StepSize { get; }
        bool AreInputsPrecalculated { get; }
    }
}
