using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.AlgorithmsControl.InformationCollectingManager;

namespace TestApp.Optimization.AlgorithmsControl.Restart.Static
{
    public class StaticRestartLauncher : OptimizationLauncher<StaticRestartLaucherParameters, RealAlgorithmDataCollector>
    {
        public override AlgorithmLauncherType LauncherType => AlgorithmLauncherType.StaticRestart;

        public override void Run()
        {
            throw new NotImplementedException();
        }
    }
}
