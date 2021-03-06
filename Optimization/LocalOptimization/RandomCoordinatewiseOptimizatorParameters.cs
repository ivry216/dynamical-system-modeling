﻿using Optimization.AlgorithmsInterfaces;

namespace Optimization.LocalOptimization
{
    public class RandomCoordinatewiseOptimizatorParameters : OptimizationAlgorithmParameters
    {
        public int NumberOfCoordinates { get => Iterations; set => Iterations = value; }
        public int NumberOfSteps { get; set; }
        public double Step { get; set; }

        public double[] InitialPoint { get; set; }
        public double InitialPointValue { get; set; }

        public RandomCoordinatewiseSearchType Type { get; set; }
    }
}
