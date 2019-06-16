using Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm.ParameterTypes;

namespace Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm
{
    public class RealGeneticAlgorithmParameters : EvolutionaryAlgorithmParameters
    {
        #region Properties

        // General parameters
        public int SizeOfTrialPopulation { get; set; }

        // Next population parameters
        public RvgaNextPopulationType NextPopulationType { get; set; }

        // Generation of new populations
        public PopulationGenerationType GenerationType { get; set; }
        public double[] GenerationFrom { get; set; }
        public double[] GenerationTo { get; set; }
        public double[] GenerationMean { get; set; }
        public double[] GenerationSd { get; set; }

        // Selection parameters
        public RvgaSelectionType SelectionType { get; set; }
        public int NumberOfParents { get; set; }
        public int TournamentSize { get; set; }

        // Crossover parameters 
        public RvgaCrossoverType CrossoverType { get; set; }

        // Mutation parameters
        public RvgaMutationType MutationType { get; set; }
        public double[] MutateFrom { get; set; }
        public double[] MutateTo { get; set; }
        public int MutationNumberOfGenes { get; set; }
        public double MutationProbability { get; set; }
        public double MutationAdditiveSD { get; set; }

        #endregion Properties

        #region Constructor

        public RealGeneticAlgorithmParameters() : base()
        { }

        #endregion Constructor
    }
}
