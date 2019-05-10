namespace TestApp.Optimization.AlgorithmsControl.AlgorithmStates
{
    public interface IBestVariableAndValueState : IAlgorithmState
    {
        double BestValue { get; }
        double[] BestSolution { get; }
    }
}
