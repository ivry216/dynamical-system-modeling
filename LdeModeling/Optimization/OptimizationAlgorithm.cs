using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.Optimization.AlgorithmsControl.AlgorithmMeta;
using TestApp.Optimization.Problem;

namespace TestApp.Optimization
{
    public abstract class OptimizationAlgorithm<AlgorithmParameters> : IOptimizationAlgorithm<AlgorithmParameters>, IRealAlgorithm, IRestartableAlgorithm
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

        public int Iteration { get; protected set; }

        object IAlgorithm.BestValue => BestValue;
        object IAlgorithm.BestSolution => BestSolution;

        #endregion Properties

        #region Constructor

        public OptimizationAlgorithm()
        {
            
        }

        #endregion Constructor

        #region Abstract Methods

        protected abstract void NextIteration();
        protected abstract void Initialize();
        protected abstract void Generate();

        #endregion Abstract Methods

        #region Restartable Methods

        void IRestartableAlgorithm.Generate() => Generate();
        void IRestartableAlgorithm.NextIteration() => NextIteration();
        
        #endregion Restartable Methods

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

        public void Evaluate()
        {
            Initialize();
            Generate();
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                NextIteration();
            }
        }

        #endregion Inherited Methods

        #region Solution-Related Methods

        protected void TryUpdateSolution(double value, double[] alternative)
        {
            if (value > BestValue)
            {
                BestValue = value;
                BestSolution.FillWithVector(alternative);
            }
        }

        #endregion Solution-Related Methods
    }
}
