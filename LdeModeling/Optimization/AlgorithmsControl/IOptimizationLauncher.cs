using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl
{
    public interface IOptimizationLauncher
    {
        IAlgorithm Algorithm { get; }
    }
}
