﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Optimization.Problem;

namespace TestApp.Optimization
{
    abstract class OptimizationAlgorithm<AlgorithmParameters> : IOptimizationAlgorithm<AlgorithmParameters>
        where AlgorithmParameters : OptimizationAlgorithmParameters
    {
        #region Fields
        protected AlgorithmParameters Parameters;
        protected OptimizationProblem Problem;
        protected int Dimension;
        #endregion Fields

        #region Properties
        public double[] BestSolution { get; protected set; }
        public double BestValue { get; protected set; }
        #endregion Properties

        #region Constructor
        public OptimizationAlgorithm()
        {
            
        }
        #endregion Constructor

        #region Abstract Methods
        public abstract void Evaluate();
        protected abstract void NextIteration();
        #endregion Abstract Methods

        #region Inherited Methods
        public void SetProblem(OptimizationProblem problem)
        {
            Problem = problem;
            Dimension = problem.Dimension;
            BestSolution = new double[Problem.Dimension];
        }

        public void SetParameters(AlgorithmParameters parameters)
        {
            Parameters = parameters;
        }
        #endregion Inherited Methods
    }
}
