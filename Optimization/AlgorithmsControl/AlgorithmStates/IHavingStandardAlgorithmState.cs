namespace Optimization.AlgorithmsControl.AlgorithmStates
{
    public interface IHavingStandardAlgorithmState : IBestVariableAndValueState
    {
        int Iteration { get; }
    }
}