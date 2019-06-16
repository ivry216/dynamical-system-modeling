using MathCore.Extensions.Arrays;
using Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics;
using Optimization.Parameters.Generation;
using Optimization.Problem.Parallel;
using System.Collections.Generic;

namespace Optimization.AlgorithmsInterfaces.Parallel
{
    public abstract class ParallelOptimizationAlgorithm<TAlgorithmParameters, TValues, TAlternatives, TCalculationResult, TAlternativeRepresentations> : IParallelOptimizationAlgorithm<TAlgorithmParameters, ParallelOptimizationProblem<TValues, TAlternatives>, TValues, TAlternatives>, IRealAlgorithm, IRestartableAlgorithm, IContainingStatsFollowers
        where TAlgorithmParameters : OptimizationAlgorithmParameters
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        #region Fields

        protected TAlgorithmParameters Parameters;
        protected ParallelOptimizationProblem<TValues, TAlternatives> Problem;
        protected int Dimension;

        protected IConverterToAlternativesForParallel<TAlternatives, TAlternativeRepresentations> converterToAlternatives;
        protected IConverterToValuesForParallel<TValues, TCalculationResult> converterToValues;

        protected abstract TAlternativeRepresentations AlternativesInIteration { get; }
        protected abstract TCalculationResult AlternativesCriterionValues { get; set ; }

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

        public ParallelOptimizationAlgorithm()
        {

        }

        #endregion Constructor

        #region Abstract Methods

        protected abstract void Iterate();
        protected abstract void Initialize();
        protected abstract void Generate();
        protected abstract void Update();

        #endregion Abstract Methods

        #region Restartable Methods

        void IIterableAlgorithm.Initialize() => Initialize();
        void IIterableAlgorithm.Generate() => Generate();
        void IIterableAlgorithm.NextIteration() => NextIteration();

        #endregion Restartable Methods

        #region Iteration Methods

        protected virtual void CalculateObjective()
        {
            var alternatives = converterToAlternatives.GetAlternatives(AlternativesInIteration);
            var solution = Problem.CalculateCriterion(alternatives);
            AlternativesCriterionValues = converterToValues.GetValues(solution);
        }

        protected virtual void NextIteration()
        {
            Iterate();
            CalculateObjective();
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
                UpdateFollowers();
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

        public void SetProblem(ParallelOptimizationProblem<TValues, TAlternatives> problem)
        {
            Problem = problem;
            Dimension = problem.Dimension;
        }

        #endregion Inherited Methods

        #region Supporting Methods

        protected void TryUpdateSolution(double value, double[] alternative)
        {
            if (value > BestValue)
            {
                BestValue = value;
                BestSolution.FillWithVector(alternative);
            }
        }

        #endregion Supporting Methods

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
