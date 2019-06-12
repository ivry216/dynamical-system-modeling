namespace Optimization.EvolutionaryAlgorithms.ParticleSwarmOptimization
{
    public class ParticleSwarmOptimizerParameters : EvolutionaryAlgorithmParameters
    {
        // Generation of new populations
        public PopulationGenerationType GenerationType { get; set; }
        public double[] GenerationFrom { get; set; }
        public double[] GenerationTo { get; set; }
        public double[] GenerationMean { get; set; }
        public double[] GenerationSd { get; set; }

        // Generation of velocities
        public double[] GenerationVelocitiesFrom { get; set; }
        public double[] GenerationVelocitiesTo { get; set; }

        // Main parameters
        public double W { get; set; }
        public double Phi1 { get; set; }
        public double Phi2 { get; set; }
    }
}
