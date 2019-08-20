using System;

namespace Optimization.Problem.Constrains.Parallel
{
    public abstract class ParallelConstrainerBase : IParallelConstrainer
    {
        protected Func<double, double[]> _objectiveFunction;

        public ParallelConstrainerBase(Func<double, double[]> objectiveFunction)
        {
            _objectiveFunction = objectiveFunction;
        }

        public abstract double Evaluate(double[] alternatives);
    }
}
