using Optimization.AlgorithmsInterfaces.Parallel;
using Optimization.Problem.Parallel.Alternatives;

namespace Optimization.EvolutionaryAlgorithms.Parallel.Converters
{
    public class ConverterToRealAlternativesByRef : IConverterToAlternativesForParallel<RealVectorAlternatives, double[][]>
    {
        public RealVectorAlternatives GetAlternatives(double[][] representations)
        {
            return new RealVectorAlternatives(representations);
        }
    }
}
