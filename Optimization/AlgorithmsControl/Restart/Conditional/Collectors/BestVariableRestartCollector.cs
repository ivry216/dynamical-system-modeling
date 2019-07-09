using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.Restart.Conditional.Collectors
{
    public class BestVariableRestartCollector
    {
        private Queue<double[]> _bestVariables;

        public IReadOnlyCollection<double[]> BestVariables => _bestVariables;

        public BestVariableRestartCollector()
        {
            _bestVariables = new Queue<double[]>();
        }

        public void AddBestAlternative(double[] bestAlternative)
        {
            _bestVariables.Enqueue(bestAlternative);
        }

        public void Reset()
        {
            _bestVariables.Clear();
        }
    }
}
