using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.Restart.Conditional.Collectors
{
    public class BestValueRestartCollector
    {
        private int _windowSize;
        private Queue<double> _bestValues;

        public IReadOnlyCollection<double> BestValues => _bestValues;

        public BestValueRestartCollector(int windowSize)
        {
            _windowSize = windowSize;
            _bestValues = new Queue<double>();
        }

        public void AddBest(double bestValue)
        {
            if (_bestValues.Count < _windowSize)
            {
                _bestValues.Enqueue(bestValue);
            }
            else
            {
                _bestValues.Dequeue();
                _bestValues.Enqueue(bestValue);
            }
        }

        public void Reset()
        {
            _bestValues.Clear();
        }
    }
}
