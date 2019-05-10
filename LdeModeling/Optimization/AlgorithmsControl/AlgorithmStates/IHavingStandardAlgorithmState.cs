using TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmStates
{
    public interface IHavingStandardAlgorithmState : IBestVariableAndValueState
    {
        int Iteration { get; }
    }
}
