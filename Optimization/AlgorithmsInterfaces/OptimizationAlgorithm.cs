using MathCore.Extensions.Arrays;
using Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics;
using Optimization.Parameters.Generation;
using Optimization.Problem;
using System.Collections.Generic;

namespace Optimization.AlgorithmsInterfaces
{
    public abstract class OptimizationAlgorithm<TAlgorithmParameters> : IOptimizationAlgorithm<TAlgorithmParameters>, IRealAlgorithm, IRestartableAlgorithm, IContainingStatsFollowers
        where TAlgorithmParameters : OptimizationAlgorithmParameters
    {
        #region Fields

        protected TAlgorithmParameters Parameters;
        protected OptimizationProblem Problem;
        protected int Dimension;

        #endregion Fields

        #region Followers Fields

        protected List<IAlgorithmIterationFollower> algorithmIterationFollowers;

        #endregion Followers Fields

        #region Interation Properties

        protected abstract double[][] IterationAlternatives { get; }
        protected abstract double[] IterationValues { get; }

        #endregion Iteration Properties

        #region Restart Properties

        IGenerationParameters IRestartableAlgorithm.GenerationParameters
        {
            get => Parameters.GenerationParameters;
            set
            {
                Parameters.GenerationParameters = value;
            }
        }

        #endregion Restart Properties

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

        #endregion Abstract Methods

        #region Generate

        protected virtual void Generate()
        {
            ResetSolution();
            // Update iteration
            Iteration = 0;
            GenerateInitial();
        }

        protected abstract void GenerateInitial();

        #endregion Generate

        #region Restartable Methods

        void IIterableAlgorithm.Generate() => Generate();
        void IIterableAlgorithm.NextIteration() => NextIteration();
        void IIterableAlgorithm.Initialize() => Initialize();
        
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
            UpdateFollowers();
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                NextIteration();
                UpdateFollowers();
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

        protected void ResetSolution()
        {
            // Update best values and solutions
            BestValue = 0;
            BestSolution = new double[Problem.Dimension];
        }

        #endregion Solution-Related Methods

        #region Stats Followers

        public void AddStatsFollowers(ICollection<IAlgorithmIterationFollower> statsFollowers)
        {
            if (algorithmIterationFollowers == null)
            {
                algorithmIterationFollowers = new List<IAlgorithmIterationFollower>();
            }

            algorithmIterationFollowers.AddRange(statsFollowers);
        }

        public void UpdateFollowers()
        {
            if (algorithmIterationFollowers != null && algorithmIterationFollowers.Count > 0)
            {
                // Perform a message to followers
                MessageToStatsFollowers message = new MessageToStatsFollowers(BestSolution, BestValue, IterationAlternatives, IterationValues);

                foreach (var follower in algorithmIterationFollowers)
                {
                    follower.Update(message);
                }
            }
        }

        #endregion Stats Followers
    }
}
