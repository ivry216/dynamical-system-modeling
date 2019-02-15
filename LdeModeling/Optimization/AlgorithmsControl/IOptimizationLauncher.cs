using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl
{
    public interface IOptimizationLauncher<TParameters>
        where TParameters : IOptimizationLauncherParameters
    {
        IRealAlgorithm Algorithm { get; set; }
        AlgorithmLauncherType LauncherType { get; }
        TParameters Parameters { get; }

        void Run();
    }
}
