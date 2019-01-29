using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.MathematicalCore.Randomizing;

namespace TestApp.Optimization.EvolutionaryAlgorithms.DifferentialAlgorithm
{
    class DifferentialEvolution : EvolutionaryAlgorithm<DifferentialEvolutionParameters>
    {
        #region Fields

        private double[] _trial;

        #endregion Fields

        #region Inherited Methods

        public override void Evaluate()
        {
            // Initialize all
            Initialize();
            // Generate the initial population
            Generate();
            // Iterate
            for (int i = 0; i < Parameters.Iterations; i++)
            {
                NextIteration();
                // Update best values
                if (Fitness[0] > BestValue)
                {
                    BestSolution.FillWithVector(Population[0]);
                    BestValue = Fitness[0];
                }
            }
        }

        protected override void Initialize()
        {
            // Call the base method
            base.Initialize();
        }

        protected override void Generate()
        {
            // Generate the new population
            // With normal distribution
            if (Parameters.GenerationType == PopulationGenerationType.Normal)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateNormallyDistributedVector(
                    dimension: Problem.Dimension,
                    mean: Parameters.GenerationMean,
                    sd: Parameters.GenerationSd);
                }
            }
            // With uniform distribution
            if (Parameters.GenerationType == PopulationGenerationType.Uniform)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateUniformlyDistributedVector(
                    dimension: Problem.Dimension,
                    from: Parameters.GenerationFrom,
                    to: Parameters.GenerationTo);
                }
            }

            // Now calculate the fitness
            CalculateFitness();
        }

        protected override void NextIteration()
        {
            // Run through all elements in population
            for (int i = 0; i < Parameters.Size; i++)
            {

            }
        }

        #endregion Inherited Methods

        #region Algorithm Methods

        //private (int indexA, int indexB, int indexC) GetCrossoverAgentsIndices()
        //{
        //    int[] indices = RandomEngine.SubsetIndexesNoRepetition
        //}

        #endregion Algorithm Methods
    }
}
