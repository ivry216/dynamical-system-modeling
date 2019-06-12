namespace Optimization.EvolutionaryAlgorithms.DifferentialAlgorithm
{
    public class DifferentialEvolutionParameters : EvolutionaryAlgorithmParameters
    {
        #region Properies

        public double CrossoverProbability { get; set; }
        public double DifferentialWeight { get; set; }
        public bool IsAtLeastOneGenMutates { get; set; }

        // Generation of new populations
        public PopulationGenerationType GenerationType { get; set; }
        public double[] GenerationFrom { get; set; }
        public double[] GenerationTo { get; set; }
        public double[] GenerationMean { get; set; }
        public double[] GenerationSd { get; set; }

        #endregion Parameters
    }
}
