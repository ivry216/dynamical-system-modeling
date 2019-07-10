using MathCore.Extensions.Arrays;
using MathCore.Vectors;
using System;
using TestApp.Models.Dynamical.DeNumericalIntegration;
using TestApp.Models.Dynamical.LinearDifferentialEquation.SingleOutput;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public class LdeSingleOutputModel : ISingleOutputLdeModel
    {
        private RungeKuttahIntegrator rungeKuttahIntegrator;

        public ISingleOutputLdeEvaluationParameters EvaluationParameters { get; set; }

        public ISingleOutputLdeModelParameters ModelParameters { get; set; }

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

        IDynamicalModelParameters INumericallyCalculable.ModelParameters
        {
            get => ModelParameters;
            set
            {
                ModelParameters = (ISingleOutputLdeModelParameters)value;
            }
        }

        private double[,] systemMatrix => ModelParameters.ModelParameters.A;
        private double[,] inputsMatrix => ModelParameters.ModelParameters.B;

        public LdeSingleOutputModel()
        {
            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
            EvaluationParameters = new LdeSingleOutputModelEvaluationParameters();
        }

        public LdeSingleOutputModel(ISingleOutputLdeEvaluationParameters evaluationParameters, ISingleOutputLdeModelParameters modelParameters)
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

        public IModelOutput Evaluate(IModelInput input)
        {
            throw new NotImplementedException();
        }

        public IDiscreteOutput Evaluate(IDiscreteInput input)
        {
            return rungeKuttahIntegrator.SolveEquation(input);
        }
    }
}