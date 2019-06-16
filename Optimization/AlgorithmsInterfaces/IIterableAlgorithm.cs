namespace Optimization.AlgorithmsInterfaces
{
    public interface IIterableAlgorithm : IAlgorithm
    {
        int Iteration { get; }
        void Initialize();
        void NextIteration();
        void Generate();
    }
}
