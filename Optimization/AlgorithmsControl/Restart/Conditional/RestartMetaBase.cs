using System;
using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public abstract class RestartMetaBase<TParameters> : IRestartMeta<TParameters>
        where TParameters : IRestartMetaParameters
    {
        protected TParameters _parameters;

        public IRestartableAlgorithm Algorithm { get; set; }

        object IAlgorithm.BestValue => throw new NotImplementedException();

        object IAlgorithm.BestSolution => throw new NotImplementedException();

        public void Evaluate()
        {
            Algorithm.Initialize();
            Algorithm.Generate();

            for (int i = 0; i < _parameters.IterationsTotal; i++)
            {
                IterateAlgorithm();

                if (IsRestartNeeded())
                {
                    Restart();
                }
            }
        }

        protected virtual void IterateAlgorithm()
        {
            Algorithm.NextIteration();
            ActAfterAlgorithmIteration();
        }

        protected abstract void ActAfterAlgorithmIteration();

        protected virtual void Restart()
        {
            ActBeforeRestart();
            Algorithm.Generate();
        }

        protected abstract void ActBeforeRestart();

        protected abstract bool IsRestartNeeded();

        void IAlgorithm.SetParameters(object parameters)
        {
            _parameters = (TParameters)parameters;
        }
    }
}
