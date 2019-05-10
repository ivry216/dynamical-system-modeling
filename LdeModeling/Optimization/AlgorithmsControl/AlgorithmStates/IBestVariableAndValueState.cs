namespace TestApp.Optimization.AlgorithmsControl.AlgorithmStates
{
    public interface IBestVariableAndValueState : IAlgorithmStats
    {
        double BestValue { get; }
        double[] BestSolution { get; }
    }
}
