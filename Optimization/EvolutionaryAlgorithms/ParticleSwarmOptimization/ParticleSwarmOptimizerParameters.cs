namespace Optimization.EvolutionaryAlgorithms.ParticleSwarmOptimization
{
    public class ParticleSwarmOptimizerParameters : EvolutionaryAlgorithmParameters
    {
        // Generation of velocities
        public double[] GenerationVelocitiesFrom { get; set; }
        public double[] GenerationVelocitiesTo { get; set; }

        // Main parameters
        public double W { get; set; }
        public double Phi1 { get; set; }
        public double Phi2 { get; set; }

        #region Constructor

        public ParticleSwarmOptimizerParameters() : base()
        { }

        #endregion Constructor
    }
}
