﻿using Optimization.AlgorithmsInterfaces;

namespace Optimization.EvolutionaryAlgorithms
{
    public class EvolutionaryAlgorithmParameters : OptimizationAlgorithmParameters
    {
        #region Properties

        public int Size { get; set; }

        #endregion Properties

        #region Constructor

        public EvolutionaryAlgorithmParameters(): base()
        {   }

        #endregion Constructor
    }
}
