﻿using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatistics
{
    public interface IBestValueStats : IAlgorithmStats
    {
        double BestValue { get; }
    }
}
