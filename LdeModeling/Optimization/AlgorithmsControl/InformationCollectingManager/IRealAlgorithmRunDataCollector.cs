using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.InformationCollectingManager
{
    public interface IRealAlgorithmRunDataCollector : IAlgorithmRunDataCollector
    {
        void AddBest(double best);
        void AddBestSolution(double[] alternative);
        new double GetBestByIndex(int index);
        new double[] GetBestSolutionByIndex(int index);
    }
}
