﻿using Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    public interface IBestAlternativeGetter
    {
        IBestVariableAndValueStats GetBestAlternativeAndValue();
    }
}
