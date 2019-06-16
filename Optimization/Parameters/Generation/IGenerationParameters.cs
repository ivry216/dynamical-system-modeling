using Optimization.EvolutionaryAlgorithms;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.Parameters.Generation
{
    public interface IGenerationParameters
    {
        // Generation of new populations
        PopulationGenerationType GenerationType { get; set; }
        double[] GenerationFrom { get; set; }
        double[] GenerationTo { get; set; }
        double[] GenerationMean { get; set; }
        double[] GenerationSd { get; set; }
    }
}
