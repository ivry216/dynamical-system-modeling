using Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics;
using System.Collections.Generic;

namespace Optimization.AlgorithmsInterfaces
{
    interface IContainingStatsFollowers
    {
        void AddStatsFollowers(ICollection<IAlgorithmIterationFollower> statsFollowers);
        void UpdateFollowers();
    }
}
