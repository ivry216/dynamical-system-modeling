using TestApp.Optimization.AlgorithmsControl.AlgorithmStates;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmMeta
{
    interface IRestartableAlgorithm : IHavingStandardAlgorithmState
    {
        void NextIteration();
        void Generate();
    }
}
