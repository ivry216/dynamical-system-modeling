using System;

namespace Optimization.Problem.Constrains.Parallel
{
    public abstract class ParallelConstrainerBase<TParameters> : IParallelConstrainer
    {
        protected Func<double, double[]> _objectiveFunction;
        protected TParameters _parameters;

        public ParallelConstrainerBase(TParameters parameters, Func<double, double[]> objectiveFunction)
        {
            _objectiveFunction = objectiveFunction;
            _parameters = parameters;
        }

        public abstract double Evaluate(double[] alternatives);
    }
}
