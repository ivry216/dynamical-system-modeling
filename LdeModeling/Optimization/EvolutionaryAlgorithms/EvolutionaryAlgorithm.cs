using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.EvolutionaryAlgorithms
{
    abstract class EvolutionaryAlgorithm<AlgorithmParameters> : OptimizationAlgorithm<AlgorithmParameters>
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

        #region Inherited Methods
        public abstract override void Evaluate();
        protected abstract override void NextIteration();
        #endregion Inherited Methods

        #region Abstract Methods
        protected abstract void Generate();
        #endregion Abstract Methods

        #region Universal Methods
        protected virtual void Initialize()
        {
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
        #endregion universal Methods
    }
}
