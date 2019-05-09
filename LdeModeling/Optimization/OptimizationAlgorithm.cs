using TestApp.Optimization.Problem;

namespace TestApp.Optimization
{
    public abstract class OptimizationAlgorithm<AlgorithmParameters> : IOptimizationAlgorithm<AlgorithmParameters>, IRealAlgorithm
        where AlgorithmParameters : OptimizationAlgorithmParameters
    {
        #region Fields
        protected AlgorithmParameters Parameters;
        protected OptimizationProblem Problem;
        protected int Dimension;
        #endregion Fields

        #region Properties
        public double[] BestSolution { get; protected set; }
        public double BestValue { get; protected set; }

        object IAlgorithm.BestValue => BestValue;
        object IAlgorithm.BestSolution => BestSolution;
        #endregion Properties

        #region Constructor
        public OptimizationAlgorithm()
        {
            
        }
        #endregion Constructor

        #region Abstract Methods
        public abstract void Evaluate();
        protected abstract void NextIteration();
        protected abstract void Initialize();
        #endregion Abstract Methods

        #region Inherited Methods
        public void SetProblem(OptimizationProblem problem)
        {
            Problem = problem;
            Dimension = problem.Dimension;
        }

        public void SetParameters(AlgorithmParameters parameters)
        {
            Parameters = parameters;
        }

        void IAlgorithm.SetParameters(object parameters)
        {
            SetParameters((AlgorithmParameters)parameters);
        }

        void IAlgorithm.Evaluate()
        {
            Evaluate();
        }

        #endregion Inherited Methods
    }
}
