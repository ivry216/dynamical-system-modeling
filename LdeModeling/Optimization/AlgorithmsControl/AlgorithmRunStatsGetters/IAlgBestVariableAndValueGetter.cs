using TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    public interface IAlgBestVariableAndValueGetter
    {
        BestVariableAndValueStats GetBestAlternativeAndValue();
    }
}
