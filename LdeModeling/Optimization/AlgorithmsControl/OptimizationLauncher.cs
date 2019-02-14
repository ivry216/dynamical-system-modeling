using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.AlgorithmsControl.InformationCollectingManager;

namespace TestApp.Optimization.AlgorithmsControl
{
    public abstract class OptimizationLauncher<TParameters, TCollector, T> : IOptimizationLauncher<TParameters>
        where TParameters : IOptimizationLauncherParameters
        where TCollector : IAlgorithmRunDataCollector, new()
        where T in <double>;
    {
        #region Fields

        protected TCollector collector;

        #endregion Fields

        #region Inherited Properties 

        public IAlgorithm Algorithm { get; }
        public OptimizationAlgorithmParameters AlgorithmParameters { get; }
        public abstract AlgorithmLauncherType LauncherType { get; }
        public TParameters Parameters { get; }

        #endregion Inherited Properties

        #region Constructor

        public OptimizationLauncher()
        {
            collector = new TCollector();
        }

        #endregion Constructor

        #region Abstract Methods

        public abstract void Run();

        #endregion Abstract Methods
    }
}