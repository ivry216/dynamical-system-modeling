﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.LocalOptimization;

namespace TestApp.Optimization.EvolutionaryAlgorithms.DifferentialAlgorithm
{
    class DifferentialEvolutionParametersWRcs : EvolutionaryAlgorithmParameters
    {
        #region Properies

        public double CrossoverProbability { get; set; }
        public double DifferentialWeight { get; set; }
        public bool IsAtLeastOneGenMutates { get; set; }

        // Generation of new populations
        public PopulationGenerationType GenerationType { get; set; }
        public double[] GenerationFrom { get; set; }
        public double[] GenerationTo { get; set; }
        public double[] GenerationMean { get; set; }
        public double[] GenerationSd { get; set; }

        // Local optimizer
        public int IndividsToOptimizeLocally { get; set; }
        public RandomCoordinatewiseOptimizatorParameters LoParameters { get; set; }

        #endregion Parameters
    }
}
