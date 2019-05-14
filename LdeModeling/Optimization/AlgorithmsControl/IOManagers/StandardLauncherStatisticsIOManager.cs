using System;
using TestApp.Optimization.AlgorithmsControl.AlgorithmMeta;
using TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace TestApp.Optimization.AlgorithmsControl.IOManagers
{
    public class StandardLauncherStatisticsIOManager : ITestSessionResultsIOManager<IContainingStats<IBestVariableAndValueStats>, IBestVariableAndValueStats>
    {
        public void SaveStats(IContainingStats<IBestVariableAndValueStats> launcher)
        {
            // Assign the stats to the new variable
            var stats = launcher.Stats;

            //
            throw new NotImplementedException();
        }
    }
}
