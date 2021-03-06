﻿using System;
using MathCore.Vectors;
using MathCore.Extensions.Arrays;
using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public class LdeModel : ILinearDifferentialEquationModel
    {
        private RungeKuttahIntegrator rungeKuttahIntegrator;

        public ILinearDifferentialEquationEvaluationParams EvaluationParameters { get; set; }

        public ILinearDifferentialEquationParameters ModelParameters { get; set; }

        IModelEvaluationParameters IModel.EvaluationParameters => EvaluationParameters;

        IModelParameters IModel.ModelParameters => ModelParameters;

        INumericalIntegrationParameters INumericallyCalculable.NumericalIntegrationParameters
        {
            get => EvaluationParameters.NumericalIntegrationParameters;
            set
            {
                EvaluationParameters.NumericalIntegrationParameters = value;
            }
        }

        double[] INumericallyCalculable.InitialState => ModelParameters.InitialState;
        int INumericallyCalculable.InputsNumber => ModelParameters.InputsNumber;
        int INumericallyCalculable.OutputsNumber => ModelParameters.OutputsNumber;

        private double[,] systemMatrix => ModelParameters.ModelParameters.A;
        private double[,] inputsMatrix => ModelParameters.ModelParameters.B;

        
        public LdeModel()
        {
            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
            EvaluationParameters = new LdeEvaluationParameters();
        }

        public LdeModel(ILinearDifferentialEquationEvaluationParams evaluationParameters, ILinearDifferentialEquationParameters modelParameters)
        {
            EvaluationParameters = evaluationParameters;
            ModelParameters = modelParameters;

            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
        }

        public double[] CalculateSystemEquation(double[] state, double[] inputs)
        {
            return VectorOperator.Sum(systemMatrix.MultiplyByVector(state), inputsMatrix.MultiplyByVector(inputs));
        }

        public IDiscreteOutput Evaluate(IContiniousInput input)
        {
            return rungeKuttahIntegrator.SolveEquation(input);
        }

        public IDiscreteOutput Evaluate(IDiscreteInput input)
        {
            return rungeKuttahIntegrator.SolveEquation(input);
        }

        public IModelOutput Evaluate(IModelInput input)
        {
            throw new NotImplementedException();
        }

        void INumericallyCalculable.InitializeModelParameters(int numberOfOutputs, int numberOfInputs, double[] initialState)
        {
            ModelParameters = new LdeModelParameters(numberOfInputs, numberOfOutputs);
            ModelParameters.InitialState = initialState;
            ModelParameters.ModelParameters = new LinearDynamicalSystemParameters(numberOfInputs, numberOfOutputs);
        }
    }
}
