﻿namespace Optimization
{
    public interface IRealAlgorithm : IAlgorithm
    {
        new double BestValue { get; }
        new double[] BestSolution { get; }
    }
}
