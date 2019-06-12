namespace Optimization.Problem.Parallel.Alternatives
{
    public class RealVectorAlternatives : IParallelOptimizationProblemAlternative
    {
        public double[][] Alternatives { get; }

        public RealVectorAlternatives(double[][] alternatives)
        {
            Alternatives = alternatives;
        }
    }
}