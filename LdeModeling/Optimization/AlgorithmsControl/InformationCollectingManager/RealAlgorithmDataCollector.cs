using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.MathematicalCore.ArrayExtensions;

namespace TestApp.Optimization.AlgorithmsControl.InformationCollectingManager
{
    public class RealAlgorithmDataCollector : IRealAlgorithmRunDataCollector
    {
        #region Fields

        private List<double> _bestValues;
        private List<double[]> _bestAlternatives;

        #endregion Fields

        #region Constructor

        public RealAlgorithmDataCollector()
        {
            _bestValues = new List<double>();
            _bestAlternatives = new List<double[]>();
        }

        #endregion Constructor

        #region Inherited Methods

        public void AddBest(double best)
        {
            _bestValues.Add(best);
        }

        public void AddBestSolution(double[] alternative)
        {
            double[] currentAlternative = new double[alternative.Length];
            currentAlternative.FillWithVector(alternative);
            _bestAlternatives.Add(currentAlternative);
        }

        public double GetBestByIndex(int index)
        {
            return _bestValues[index];
        }

        public double[] GetBestSolutionByIndex(int index)
        {
            return _bestAlternatives[index];
        }

        void IAlgorithmRunDataCollector.AddBest(object best)
        {
            AddBest((double)best);
        }

        void IAlgorithmRunDataCollector.AddBestSolution(object alternative)
        {
            AddBestSolution((double[])alternative);
        }

        object IAlgorithmRunDataCollector.GetBestByIndex(int index)
        {
            return GetBestByIndex(index);
        }

        object IAlgorithmRunDataCollector.GetBestSolutionByIndex(int index)
        {
            return GetBestSolutionByIndex(index);
        }

        #endregion Inherited Methods
    }
}
