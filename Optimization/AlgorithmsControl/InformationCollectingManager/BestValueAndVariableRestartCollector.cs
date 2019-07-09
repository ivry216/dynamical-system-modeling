using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.AlgorithmsControl.InformationCollectingManager
{
    public class BestValueAndVariableRestartCollector : AlgorithmRunDataCollector<IBestVariableAndValueStats>
    {
        private int _windowSize;
        private Queue<double> _bestValues;

        public List<double[]> Variables { get; set; }

        public BestValueAndVariableRestartCollector() : base()
        {
            Variables = new List<double[]>();
            _bestValues = new Queue<double>();
        }

        public void MakeWindow(int windowSize)
        {
            _windowSize = windowSize;
        }

        public void Refresh()
        {
            _bestValues.Clear();
            Variables.Clear();
        }

        protected override void ActWhenAdd(IBestVariableAndValueStats statsToAdd)
        {
            base.ActWhenAdd(statsToAdd);

            if (_bestValues.Count < _windowSize)
            {
                _bestValues.Enqueue(statsToAdd.BestValue);
            }
            else
            {
                _bestValues.Dequeue();
                _bestValues.Enqueue(statsToAdd.BestValue);
            }
        }
    }
}
