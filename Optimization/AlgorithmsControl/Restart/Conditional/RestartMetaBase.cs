using System;
using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public abstract class RestartMetaBase<TParameters> : IRestartMeta<TParameters>
        where TParameters : IRestartMetaParameters
    {
        protected double _bestValue;
        protected double[] _bestAlternative;

        public TParameters Parameters { get; set; }

        public IRestartableAlgorithm Algorithm { get; set; }

        public double BestValue => _bestValue;

        public double[] BestSolution => _bestAlternative;

        object IAlgorithm.BestValue => BestValue;

        object IAlgorithm.BestSolution => BestSolution;

        public void Evaluate()
        {
            InitializeCollectors();

            Algorithm.Initialize();
            Algorithm.Generate();

            for (int i = 0; i < Parameters.IterationsTotal; i++)
            {
                IterateAlgorithm();

                if (IsRestartNeeded())
                {
                    Restart();
                }
            }

            UpdateSolution(Algorithm.BestValue, Algorithm.BestSolution);
        }

        protected abstract void InitializeCollectors();

        protected virtual void IterateAlgorithm()
        {
            Algorithm.NextIteration();
            ActAfterAlgorithmIteration();
        }

        protected abstract void ActAfterAlgorithmIteration();

        protected virtual void Restart()
        {
            UpdateSolution(Algorithm.BestValue, Algorithm.BestSolution);
            ActBeforeRestart();
            Algorithm.Generate();
        }

        protected abstract void ActBeforeRestart();

        protected abstract bool IsRestartNeeded();

        void IAlgorithm.SetParameters(object parameters)
        {
            Parameters = (TParameters)parameters;
        }

        protected void UpdateSolution(double value, double[] alternative)
        {
            if (value > _bestValue)
            {
                _bestValue = value;
                _bestAlternative = alternative;
            }
        }
    }
}
