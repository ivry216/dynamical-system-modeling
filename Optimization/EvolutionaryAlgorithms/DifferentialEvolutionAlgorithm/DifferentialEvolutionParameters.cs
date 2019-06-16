namespace Optimization.EvolutionaryAlgorithms.DifferentialEvolutionAlgorithm
{
    public class DifferentialEvolutionParameters : EvolutionaryAlgorithmParameters
    {
        #region Properies

        public double CrossoverProbability { get; set; }
        public double DifferentialWeight { get; set; }
        public bool IsAtLeastOneGenMutates { get; set; }

        #endregion Parameters

        #region Constructor

        public DifferentialEvolutionParameters() : base()
        { }

        #endregion Constructor
    }
}
