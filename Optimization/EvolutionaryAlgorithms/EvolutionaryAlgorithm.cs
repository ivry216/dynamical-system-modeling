﻿using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure;
using Optimization.AlgorithmsInterfaces;

namespace Optimization.EvolutionaryAlgorithms
{
    public abstract class EvolutionaryAlgorithm<AlgorithmParameters> : OptimizationAlgorithm<AlgorithmParameters>, IBestAlternativeAndValueGetter
        where AlgorithmParameters : EvolutionaryAlgorithmParameters
    {
        #region Fields

        protected double[][] Population;
        protected double[] Fitness;

        protected double[][] TrialPopulation;
        protected double[] TrialFitness;

        protected double[][] MergedPopulation;
        protected double[] MergedFitness;

        #endregion Fields

        #region Iteration Properties

        protected override double[][] IterationAlternatives => Population;
        protected override double[] IterationValues => Fitness;

        #endregion Iteration Properties

        #region Universal Methods

        protected override void Initialize()
        {
            // Update best values and solutions
            BestValue = 0;
            BestSolution = new double[Problem.Dimension];

            // Initialize the population
            Population = new double[Parameters.Size][];
            for (int i = 0; i < Problem.Dimension; i++)
            {
                Population[i] = new double[Problem.Dimension];
            }

            // Initialize the fitness
            Fitness = new double[Parameters.Size];
        }

        protected void CalculateFitness()
        {
            // Calculate fitness for all te individs
            for (int i = 0; i < Parameters.Size; i++)
            {
                Fitness[i] = Problem.CalcualteCriterion(Population[i]);
            }
        }

        IBestVariableAndValueStats IBestAlternativeAndValueGetter.GetBestAlternativeAndValue()
        {
            return new BestVariableAndValueStats(BestValue, BestSolution);
        }

        #endregion universal Methods
    }
}
