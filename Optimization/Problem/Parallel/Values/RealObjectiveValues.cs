using System.Collections.Concurrent;

namespace Optimization.Problem.Parallel.Values
{
    public class RealObjectiveValues : IParallelOptimizationProblemValues
    {
        ConcurrentBag<(int, double)> Values { get; }

        public RealObjectiveValues(ConcurrentBag<(int, double)> values)
        {
            Values = values;
        }
    }
}
