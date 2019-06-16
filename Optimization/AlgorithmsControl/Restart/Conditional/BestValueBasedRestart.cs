using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using Optimization.AlgorithmsControl.InformationCollectingManager;
using Optimization.AlgorithmsInterfaces;
using System;

namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public class BestValueBasedRestart : OptimizationLauncher<BestValueBasedRestartParameters, RealAlgorithmDataCollector, IBestVariableAndValueStats, IRealIterableAlgorithm>
    {
        public double BestValue => throw new NotImplementedException();

        public double[] BestSolution => throw new NotImplementedException();

        public BestValueBasedRestart(BestValueBasedRestartParameters parameters) : base(parameters)
        { }

        public void Evaluate()
        {
            throw new NotImplementedException();
        }

        public override void Run()
        {
            throw new NotImplementedException();
        }

        public void SetParameters(object parameters)
        {
            throw new NotImplementedException();
        }
    }
}
