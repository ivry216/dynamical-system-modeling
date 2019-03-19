﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataSample;
using TestApp.Models.Dynamical.LinearDifferentialEquation;
using TestApp.Models.Dynamical.ModelToDataProcessing;
using TestApp.Models.Dynamical.SamplePreprocessing;
using TestApp.Optimization.Problem;

namespace TestApp.Models.Dynamical.InverseProblem
{
    public abstract class LdeInverseProblem : OptimizationProblem
    {
        #region Fields

        protected SampleToLdeDataProcessor sampleToLdeProcessor;
        protected ModelToDataProcessor modelToDataProcessor;
        protected LdeModel model;

        #endregion Fields

        #region Properties

        public int NumberOfStateVars { get; set; }
        public int NumberOfInputVars { get; set; }
        public double[] InitialValue { get; private set; }

        #endregion Properties

        #region Constructor

        public LdeInverseProblem(int dimension) : base(dimension)
        {
            sampleToLdeProcessor = new SampleToLdeDataProcessor();
            modelToDataProcessor = new ModelToDataProcessor();
            model = new LdeModel();
        }

        #endregion Construtor

        #region Setting

        public void SetData(DynamicalSystemSample sample)
        {
            sampleToLdeProcessor.Process(sample);
            modelToDataProcessor.SetData(sample);
            var ldeEvaluationParameters = modelToDataProcessor.SetIntegrationSchemeBySample();
            model.EvaluationParameters = ldeEvaluationParameters;
            InitialValue = sample.Data.InitialValue;
        }

        public void SetInputs(IDynamicalModelInput discreteDynamicalModelInput)
        {
            modelToDataProcessor.SetInputs(discreteDynamicalModelInput);
            NumberOfInputVars = discreteDynamicalModelInput.NumberOfInputs;
        }

        public void InitializeCalculation()
        {
            model.ModelParameters = new LdeModelParameters(NumberOfStateVars, NumberOfInputVars);
            model.ModelParameters.InitialState = InitialValue;
            model.ModelParameters.ModelParameters = new LinearDynamicalSystemParameters(NumberOfInputVars, NumberOfStateVars);
        }

        public override abstract double CalcualteCriterion(double[] alternative);

        #endregion Setting
    }
}
