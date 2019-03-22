using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.LocalOptimization
{
    public class RandomCoordinatewiseOptimizatorParameters : IOptimizationAlgorithmParameters
    {
        public int NumberOfCoordinates { get; set; }
        public int NumberOfSteps { get; set; }
        public double Step { get; set; }

        public double[] InitialPoint { get; set; }
        public double InitialPointValue { get; set; }

        public RandomCoordinatewiseSearchType Type { get; set; }
    }
}
