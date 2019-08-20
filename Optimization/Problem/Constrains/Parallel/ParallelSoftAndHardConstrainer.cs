using Optimization.Problem.Constrains.Parallel.SoftAndHard;
using System;

namespace Optimization.Problem.Constrains.Parallel
{
    public class ParallelSoftAndHardConstrainer : ParallelConstrainerBase<SahConstrainerParameters>
    {
        public ParallelSoftAndHardConstrainer(SahConstrainerParameters parameters, Func<double, double[]> objectiveFunction) : base(parameters, objectiveFunction)
        {
        }

        public override double Evaluate(double[] alternatives)
        {
            throw new NotImplementedException();
        }
    }
}
