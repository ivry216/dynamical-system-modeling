using System;

namespace Optimization.Problem.Constrains.Parallel
{
    public class ParallelSoftAndHardConstrainer : ParallelConstrainerBase
    {
        public ParallelSoftAndHardConstrainer(Func<double, double[]> objectiveFunction) : base(objectiveFunction)
        {
        }

        public override double Evaluate(double[] alternatives)
        {
            throw new NotImplementedException();
        }
    }
}
