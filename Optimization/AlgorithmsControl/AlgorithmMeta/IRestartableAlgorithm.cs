using Optimization.AlgorithmsControl.AlgorithmStates;

namespace Optimization.AlgorithmsControl.AlgorithmMeta
{
    interface IRestartableAlgorithm : IHavingStandardAlgorithmState
    {
        void NextIteration();
        void Generate();
    }
}
