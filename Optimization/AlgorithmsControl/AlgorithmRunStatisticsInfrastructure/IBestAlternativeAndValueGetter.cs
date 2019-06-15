using Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure
{
    public interface IBestAlternativeAndValueGetter
    {
        IBestVariableAndValueStats GetBestAlternativeAndValue();
    }
}
