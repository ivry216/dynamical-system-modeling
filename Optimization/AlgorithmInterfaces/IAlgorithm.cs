namespace Optimization.AlgorithmsInterfaces
{
    public interface IAlgorithm
    {
        object BestValue { get; }
        object BestSolution { get; }
        void SetParameters(object parameters);
        void Evaluate();
    }
}
