using System.Collections.Concurrent;

namespace Optimization.Problem.Parallel.Values
{
    public class RealObjectiveValues : IParallelOptimizationProblemValues
    {
        public ConcurrentBag<(int Index, double Value)> Values { get; }

        public RealObjectiveValues(ConcurrentBag<(int, double)> values)
        {
            Values = values;
        }
    }
}
