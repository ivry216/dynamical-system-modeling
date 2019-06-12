﻿using System.Collections.Generic;
using System.Linq;
using Optimization.AlgorithmsControl.AlgorithmMeta;
using Optimization.AlgorithmsControl.InformationCollectingManager;
using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl
{
    public abstract class OptimizationLauncher<TParameters, TCollector, TStats> : IOptimizationLauncher<TParameters, TStats>, IContainingStats<TStats>
        where TStats : IAlgorithmStats
        where TParameters : IOptimizationLauncherParameters
        where TCollector : IAlgorithmRunDataCollector<TStats>, new()
    {
        #region Fields

        protected TCollector collector;

        #endregion Fields

        #region Inherited Properties 

        public List<TStats> Stats => collector.GetAll().ToList();
        public IRealAlgorithm Algorithm { get; set; }
        public TParameters Parameters { get; }

        #endregion Inherited Properties

        #region Constructor

        public OptimizationLauncher(TParameters parameters)
        {
            collector = new TCollector();

            Parameters = parameters;
        }

        #endregion Constructor

        #region Abstract Methods

        public abstract void Run();

        #endregion Abstract Methods
    }
}