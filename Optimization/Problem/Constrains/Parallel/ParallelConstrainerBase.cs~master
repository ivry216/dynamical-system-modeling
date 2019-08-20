using Optimization.Problem.Parallel;
using System;

namespace Optimization.Problem.Constrains.Parallel
{
    public abstract class ConstrainerBase<TValues, TAlternatives> : IConstrainer<TValues, TAlternatives>
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        protected Func<TValues, TAlternatives> _objectiveFunction;

        public ConstrainerBase(Func<TValues, TAlternatives> objectiveFunction)
        {
            _objectiveFunction = objectiveFunction;
        }

        public abstract TValues Evaluate(TAlternatives alternatives);
    }
}
