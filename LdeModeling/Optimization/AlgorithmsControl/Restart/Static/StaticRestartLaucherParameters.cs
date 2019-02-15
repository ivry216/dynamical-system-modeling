using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.AlgorithmsControl.Restart.Static
{
    public class StaticRestartLaucherParameters : IOptimizationLauncherParameters
    {
        public int Iterations { get; set; }
    }
}
