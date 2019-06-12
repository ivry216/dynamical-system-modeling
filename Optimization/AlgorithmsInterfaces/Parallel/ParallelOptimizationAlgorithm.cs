using Optimization.AlgorithmsControl.AlgorithmMeta;
using Optimization.Problem.Parallel;

namespace Optimization.AlgorithmsInterfaces.Parallel
{
    public abstract class ParallelOptimizationAlgorithm<TAlgorithmParameters> : IParallelOptimizationAlgorithm<TAlgorithmParameters, ParallelOptimizationProblem<IParallelOptimizationProblemValues, IParallelOptimizationProblemAlternative>, IParallelOptimizationProblemValues, IParallelOptimizationProblemAlternative>, IRealAlgorithm, IRestartableAlgorithm
        where TAlgorithmParameters : OptimizationAlgorithmParameters
    {
        #region Fields

        protected TAlgorithmParameters Parameters;
        protected ParallelOptimizationProblem<IParallelOptimizationProblemValues, IParallelOptimizationProblemAlternative> Problem;
        protected int Dimension;

        protected IConverterToAlternativesForParallel converterToAlternatives;
        protected IConverterToValuesForParallel converterToValues;

        #endregion Fields

        #region Properties

        public double[] BestSolution { get; protected set; }
        public double BestValue { get; protected set; }

        public int Iteration { get; protected set; }

        object IAlgorithm.BestValue => BestValue;
        object IAlgorithm.BestSolution => BestSolution;

        #endregion Properties

        #region Constructor

        public ParallelOptimizationAlgorithm()
        {

        }

        #endregion Constructor

        #region Abstract Methods

        protected abstract void Iterate();
        protected abstract void Initialize();
        protected abstract void Generate();
        protected abstract void CalculateFitness();

        #endregion Abstract Methods

        #region Restartable Methods

        void IRestartableAlgorithm.Generate() => Generate();
        void IRestartableAlgorithm.NextIteration() => NextIteration();

        #endregion Restartable Methods

        #region Iteration Methods

        protected virtual void NextIteration()
        {
            Iterate();
            CalculateFitness();
        }

        #endregion Iteration Methods

        #region Inherited Methods

        public void Evaluate()
        {
            Initialize();
            Generate();
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                NextIteration();
            }
        }

        public void SetParameters(TAlgorithmParameters parameters)
        {
            Parameters = parameters;
        }

        void IAlgorithm.SetParameters(object parameters)
        {
            SetParameters((TAlgorithmParameters)parameters);
        }

        public void SetParameters(object parameters)
        {
            SetParameters((TAlgorithmParameters)parameters);
        }

        public void SetProblem(ParallelOptimizationProblem<IParallelOptimizationProblemValues, IParallelOptimizationProblemAlternative> problem)
        {
            Problem = problem;
            Dimension = problem.Dimension;
        }

        #endregion Inherited Methods
    }
}
