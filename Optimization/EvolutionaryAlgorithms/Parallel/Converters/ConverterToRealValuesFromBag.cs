using Optimization.AlgorithmsInterfaces.Parallel;
using Optimization.Problem.Parallel.Values;

namespace Optimization.EvolutionaryAlgorithms.Parallel.Converters
{
    public class ConverterToRealValuesFromBag : IConverterToValuesForParallel<RealObjectiveValues, double[]>
    {
        public double[] GetValues(RealObjectiveValues calculationResult)
        {
            double[] convertionResult = new double[calculationResult.Values.Count];
            var indexValuePairs = calculationResult.Values.ToArray();

            for (int i = 0; i < indexValuePairs.Length; i++)
            {
                convertionResult[indexValuePairs[i].Index] = indexValuePairs[i].Value;
            }

            return convertionResult;
        }
    }
}
