namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public class BestValueBasedRestartParameters : IRestartMetaParameters
    {
        public int IterationsTotal { get; set; }
        public RestartType Type { get; }

        public int WindowSize { get; set; }
        public double Threshold { get; set; }

        public double BestSolutionRestartDistance { get; set; }
    }
}