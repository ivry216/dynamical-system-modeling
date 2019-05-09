using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IBestVariableAndValueStats : IAlgorithmStats
    {
        double BestValue { get; }
        double[] BestAlternative { get; }
    }
}
