using System.Linq;
using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.MathematicalCore.Randomizing;

namespace TestApp.Optimization.EvolutionaryAlgorithms.DifferentialAlgorithm
{
    class DifferentialEvolution : EvolutionaryAlgorithm<DifferentialEvolutionParameters>
    {
        #region Fields

        private double[] _trial;
        private int[][] _indicesWithExcludedOne;

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

            // Initialize indixes with excluded one (for crossover)
            _indicesWithExcludedOne = new int[Parameters.Size][];
            // Initialize sequence
            var sequence = Enumerable.Range(0, Parameters.Size);
            for (int i = 0; i < Parameters.Size; i++)
            {
                _indicesWithExcludedOne[i] = sequence.Where(number => number != i).ToArray();
            }

            // Initialize the trial variable
            _trial = new double[Problem.Dimension];
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
                // Indices for recombinants
                int[] indices = GetCrossoverAgentsIndices(individIndex: i);
                // Calcualte trial solution
                double[] trial = PerformCrossover(individIndex: i, recombinantsIndices: indices);
                // Get the fitness of the trial solution
                double fitnessOfTrial = Problem.CalcualteCriterion(trial);

                // Update the population if the new solution is better
                if (fitnessOfTrial > Fitness[i])
                {
                    Population[i] = trial;
                    Fitness[i] = fitnessOfTrial;

                    // Update the best solution
                    TryUpdateSolution(fitnessOfTrial, trial);
                }
            }
        }

        #endregion Inherited Methods

        #region Algorithm Methods

        private int[] GetCrossoverAgentsIndices(int individIndex)
        {
            return RandomEngine.Instance.SubsetWithNoRepetitions(_indicesWithExcludedOne[individIndex], subsetSize: 3, ordered: true);
        }

        private double[] PerformCrossover(int individIndex, int[] recombinantsIndices)
        {
            // Initialize the trial solution
            double[] trial = new double[Problem.Dimension];

            switch(Parameters.IsAtLeastOneGenMutates)
            {
                case true:
                    {
                        // Generate the random index
                        int randIndex = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, Problem.Dimension);
                        // Perform trial solution
                        for (int i = 0; i < Problem.Dimension; i++)
                        {
                            if (RandomEngine.Instance.GenerateUniformlyDistributed() < Parameters.CrossoverProbability || i == randIndex)
                            {
                                trial[i] = Population[recombinantsIndices[0]][i] + Parameters.DifferentialWeight * (Population[recombinantsIndices[1]][i] - Population[recombinantsIndices[2]][i]);
                            }
                            else
                            {
                                trial[i] = Population[individIndex][i];
                            }
                        }
                        break;
                    }
                case false:
                    {
                        // Perform trial solution
                        for (int i = 0; i < Problem.Dimension; i++)
                        {
                            if (RandomEngine.Instance.GenerateUniformlyDistributed() < Parameters.CrossoverProbability)
                            {
                                trial[i] = Population[recombinantsIndices[0]][i] + Parameters.DifferentialWeight * (Population[recombinantsIndices[1]][i] - Population[recombinantsIndices[2]][i]);
                            }
                            else
                            {
                                trial[i] = Population[individIndex][i];
                            }
                        }
                        break;
                    }
            }
            
            return trial;
        }

        #endregion Algorithm Methods
    }
}
