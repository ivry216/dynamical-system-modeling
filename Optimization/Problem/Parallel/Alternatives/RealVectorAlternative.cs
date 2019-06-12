namespace Optimization.Problem.Parallel.Alternatives
{
    public class RealVectorAlternatives : IParallelOptimizationProblemAlternative
    {
        double[][] Alternatives { get; }

        public RealVectorAlternatives(double[][] alternatives)
        {
            Alternatives = alternatives;
        }
    }
}