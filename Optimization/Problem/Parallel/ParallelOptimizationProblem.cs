namespace Optimization.Problem.Parallel
{
    public abstract class ParallelOptimizationProblem<TValues, TAlternatives> : IParallelOptimizationProblem<TValues, TAlternatives>
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        #region Properties

        public int Dimension { get; }
        public bool IsConstrained { get; }

        #endregion Properties

        #region Constructor

        public ParallelOptimizationProblem(int dimension)
        {
            Dimension = dimension;
        }

        #endregion Construtor

        #region Inherited Methods

        public abstract TValues CalculateCriterion(TAlternatives alternatives);

        #endregion Inherited Methods
    }
