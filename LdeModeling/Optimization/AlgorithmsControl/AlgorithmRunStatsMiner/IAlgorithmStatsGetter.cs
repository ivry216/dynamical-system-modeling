using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatsMiner
{
    public interface IAlgorithmStatsGetter<TStats>
        where TStats : IAlgorithmStats
    {
        TStats GetStats();
    }
}
