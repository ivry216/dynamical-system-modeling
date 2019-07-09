namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public interface IRestartMetaParameters
    {
        int IterationsTotal { get; set; }
        RestartType Type { get; }
    }
}
