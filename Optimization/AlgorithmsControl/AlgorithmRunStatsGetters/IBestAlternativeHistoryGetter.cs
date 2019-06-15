﻿using Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatsGetters
{
    public interface IBestAlternativeHistoryGetter
    {
        IBestAlternativeHistoryStats GetAlternativeHistory();
    }
}