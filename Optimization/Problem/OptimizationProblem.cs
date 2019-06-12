namespace Optimization.Problem
{
    public abstract class OptimizationProblem : IOptimizationProblem
    {
        #region Properties

        public int Dimension { get; }
        public bool IsConstrained { get; }

        #endregion Properties

        #region Constructor

        public OptimizationProblem(int dimension)
        {
            Dimension = dimension;
        }

        #endregion Construtor

        #region Inherited Methods

        public abstract double CalcualteCriterion(double[] alternative);

        #endregion Inherited Methods
    }
}
