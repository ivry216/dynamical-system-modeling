using Optimization.Problem.Constrains;

namespace Optimization.Problem.Parallel.Constrained
{
    public abstract class ParallelOptimizationConstrainedProblem<TValues, TAlternatives> : IParallelOptimizationProblem<TValues, TAlternatives>
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        #region Fields

        protected HardAndSoftConstrainer _constrainer;

        #endregion Fields

        #region Properties

        public int Dimension { get; }
        public bool IsConstrained => true;

        #endregion Properties

        #region Constructor

        public ParallelOptimizationConstrainedProblem(int dimension, HardAndSoftConstrainer constrainer)
        {
            Dimension = dimension;
            _constrainer = constrainer;
        }

        #endregion Construtor

        #region Inherited Methods

        public abstract TValues CalculateCriterion(TAlternatives alternatives);

        #endregion Inherited Methods
    }
}
