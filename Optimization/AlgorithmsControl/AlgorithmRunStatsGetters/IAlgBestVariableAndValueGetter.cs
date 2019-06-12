using Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    public interface IAlgBestVariableAndValueGetter
    {
        BestVariableAndValueStats GetBestAlternativeAndValue();
    }
}
