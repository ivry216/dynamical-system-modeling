using Optimization.EvolutionaryAlgorithms;

namespace Optimization.Parameters.Generation
{
    public class GenerationParameters : IGenerationParameters
    {
        public PopulationGenerationType GenerationType { get; set; }
        public double[] GenerationFrom { get; set; }
        public double[] GenerationTo { get; set; }
        public double[] GenerationMean { get; set; }
        public double[] GenerationSd { get; set; }

        public GenerationParameters()
        {

        }
    }
}
