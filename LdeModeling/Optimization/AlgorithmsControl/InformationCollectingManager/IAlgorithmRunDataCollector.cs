using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.InformationCollectingManager
{
    public interface IAlgorithmRunDataCollector
    {
        void AddBest(object best);
        void AddBestSolution(object alternative);
        object GetBestByIndex(int index);
        object GetBestSolutionByIndex(int index);
    }
}
