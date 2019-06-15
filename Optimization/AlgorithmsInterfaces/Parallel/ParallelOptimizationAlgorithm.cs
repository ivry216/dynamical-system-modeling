﻿using MathCore.Extensions.Arrays;
using Optimization.AlgorithmsControl.AlgorithmMeta;
using Optimization.Problem.Parallel;

namespace Optimization.AlgorithmsInterfaces.Parallel
{
    public abstract class ParallelOptimizationAlgorithm<TAlgorithmParameters, TValues, TAlternatives, TCalculationResult, TAlternativeRepresentations> : IParallelOptimizationAlgorithm<TAlgorithmParameters, ParallelOptimizationProblem<TValues, TAlternatives>, TValues, TAlternatives>, IRealAlgorithm, IRestartableAlgorithm
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

        void IRestartableAlgorithm.Generate() => Generate();
        void IRestartableAlgorithm.NextIteration() => NextIteration();

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
    }
}