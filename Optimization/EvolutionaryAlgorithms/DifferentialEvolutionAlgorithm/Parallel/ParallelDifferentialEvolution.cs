using MathCore.Extensions.Arrays;
using Optimization.EvolutionaryAlgorithms.Parallel;
using Optimization.EvolutionaryAlgorithms.Parallel.Converters;
using Optimization.Problem.Parallel.Alternatives;
using Optimization.Problem.Parallel.Values;
using Randomizer.Randomizing;
using System.Linq;

namespace Optimization.EvolutionaryAlgorithms.DifferentialEvolutionAlgorithm.Parallel
{
    public class ParallelDifferentialEvolution : ParallelEvolutionaryAlgorithm<DifferentialEvolutionParameters, RealObjectiveValues, RealVectorAlternatives, double[], double[][]>
    {
        #region Fields

        private double[][] _trials;
        private double[] _trialFitnesses;
        private int[][] _indicesWithExcludedOne;

        #endregion Fields

        #region Inner Properties

        protected override double[][] AlternativesInIteration { get => _trials; }
        protected override double[] AlternativesCriterionValues {
            get => _trialFitnesses;
            set
            {
                _trialFitnesses = value;
            }
        }

        #endregion Inner Properties

        #region Constructor

        public ParallelDifferentialEvolution()
        {
            converterToAlternatives = new ConverterToRealAlternativesByRef();
            converterToValues = new ConverterToRealValuesFromBag();
        }

        #endregion Constructor

        #region Inherited Methods

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
            _trials = new double[Parameters.Size][];
            for (int i = 0; i < Parameters.Size; i++)
            {
                _trials[i] = new double[Problem.Dimension];
            }
        }

        protected override void Generate()
        {
            // Update iteration
            Iteration = 0;

            // Generate the new population
            // With normal distribution
            if (Parameters.GenerationParameters.GenerationType == PopulationGenerationType.Normal)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateNormallyDistributedVector(
                    dimension: Problem.Dimension,
                    mean: Parameters.GenerationParameters.GenerationMean,
                    sd: Parameters.GenerationParameters.GenerationSd);
                }
            }
            // With uniform distribution
            if (Parameters.GenerationParameters.GenerationType == PopulationGenerationType.Uniform)
            {
                for (int i = 0; i < Parameters.Size; i++)
                {
                    Population[i] = RandomEngine.Instance.GenerateUniformlyDistributedVector(
                    dimension: Problem.Dimension,
                    from: Parameters.GenerationParameters.GenerationFrom,
                    to: Parameters.GenerationParameters.GenerationTo);
                }
            }

            // Now calculate the fitness
            for (int i = 0; i < Parameters.Size; i++)
            {
                _trials[i].FillWithVector(Population[i]);
            }
            CalculateObjective();
            Fitness.FillWithVector(_trialFitnesses);

            // Get best value
            for (int i = 0; i < Parameters.Size; i++)
            {
                TryUpdateSolution(Fitness[i], Population[i]);
            }
        }

        protected override void Iterate()
        {
            // Update iteration
            Iteration++;

            // Run through all elements in population
            for (int i = 0; i < Parameters.Size; i++)
            {
                // Indices for recombinants
                int[] indices = GetCrossoverAgentsIndices(individIndex: i);
                // Calcualte trial solution
                _trials[i] = PerformCrossover(individIndex: i, recombinantsIndices: indices);
            }
        }

        protected override void Update()
        {
            // Run through all elements in population
            for (int i = 0; i < Parameters.Size; i++)
            {
                if (_trialFitnesses[i] > Fitness[i])
                {
                    Fitness[i] = _trialFitnesses[i];
                    Population[i].FillWithVector(_trials[i]);

                    TryUpdateSolution(Fitness[i], Population[i]);
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

            switch (Parameters.IsAtLeastOneGenMutates)
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