using Optimization.LocalOptimization;

namespace Optimization.EvolutionaryAlgorithms.DifferentialEvolutionAlgorithm
{
    public class DifferentialEvolutionParametersWRcs : EvolutionaryAlgorithmParameters
    {
        #region Properies

        public double CrossoverProbability { get; set; }
        public double DifferentialWeight { get; set; }
        public bool IsAtLeastOneGenMutates { get; set; }

        // Local optimizer
        public int IndividsToOptimizeLocally { get; set; }
        public RandomCoordinatewiseOptimizatorParameters LoParameters { get; set; }

        #endregion Parameters

        #region Constructor

        public DifferentialEvolutionParametersWRcs() : base()
        { }

        #endregion Constructor
    }
}
