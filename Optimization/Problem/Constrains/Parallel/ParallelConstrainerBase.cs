using Optimization.Problem.Parallel;
using System;

namespace Optimization.Problem.Constrains.Parallel
{
    public abstract class ParallelConstrainerBase<TValues, TAlternatives> : IParallelConstrainer<TValues, TAlternatives>
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        protected Func<TValues, TAlternatives> _objectiveFunction;

        public ParallelConstrainerBase(Func<TValues, TAlternatives> objectiveFunction)
        {
            _objectiveFunction = objectiveFunction;
        }

        public abstract TValues Evaluate(TAlternatives alternatives);
    }
}
