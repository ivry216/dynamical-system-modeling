using TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatsMiner
{
    public interface IAlgBestVariableAndValueGetter
    {
        BestVariableAndValueStats GetBestAlternativeAndValue();
    }
}
