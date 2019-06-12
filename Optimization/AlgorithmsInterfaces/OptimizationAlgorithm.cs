using MathCore.Extensions.Arrays;
using Optimization.AlgorithmsControl.AlgorithmMeta;
using Optimization.Problem;

namespace Optimization.AlgorithmsInterfaces
{
    public abstract class OptimizationAlgorithm<TAlgorithmParameters> : IOptimizationAlgorithm<TAlgorithmParameters>, IRealAlgorithm, IRestartableAlgorithm
        where TAlgorithmParameters : OptimizationAlgorithmParameters
    {
        #region Fields

        protected TAlgorithmParameters Parameters;
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

        public void SetParameters(TAlgorithmParameters parameters)
        {
            Parameters = parameters;
        }

        void IAlgorithm.SetParameters(object parameters)
        {
            SetParameters((TAlgorithmParameters)parameters);
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
