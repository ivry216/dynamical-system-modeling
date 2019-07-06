using System;
using TestApp.Models.Dynamical.DeNumericalIntegration;
using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.SystemsS
{
    // TODO: class is partly the same with LdeModel
    public class SSystemModel : ISSystemModel
    {
        private RungeKuttahIntegrator rungeKuttahIntegrator;

        public ISSystemModelEvaluationParameters EvaluationParameters { get; set; }

        public ISSystemModelParameters ModelParameters { get; set; }

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
                ModelParameters = (ISSystemModelParameters)value;
            }
        }

        public SSystemModel()
        {
            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
            EvaluationParameters = new SSystemEvaluationParameters();
        }

        public SSystemModel(ISSystemModelEvaluationParameters evaluationParameters, ISSystemModelParameters modelParameters)
        {
            EvaluationParameters = evaluationParameters;
            ModelParameters = modelParameters;

            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
        }

        public IDiscreteOutput Evaluate(IContiniousInput input)
        {
            return rungeKuttahIntegrator.SolveEquation(input);
        }

        public IModelOutput Evaluate(IModelInput input)
        {
            throw new System.NotImplementedException();
        }

        public double[] CalculateSystemEquation(double[] state, double[] inputs)
        {
            double[] currentState = new double[ModelParameters.OutputsNumber];

            for (int i = 0; i < ModelParameters.OutputsNumber; i++)
            {
                currentState[i] = ModelParameters.ModelParameters.Alpha[i];
                for (int j = 0; j < ModelParameters.OutputsNumber; j++)
                {
                    currentState[i] *= Math.Pow(state[j], ModelParameters.ModelParameters.G[i, j]);
                }
                for (int j = 0, k = ModelParameters.OutputsNumber; j < ModelParameters.InputsNumber; j++, k++)
                {
                    currentState[i] *= Math.Pow(inputs[j], ModelParameters.ModelParameters.G[i, k]);
                }

                double trial = ModelParameters.ModelParameters.Betta[i];
                for (int j = 0; j < ModelParameters.OutputsNumber; j++)
                {
                    trial *= Math.Pow(state[j], ModelParameters.ModelParameters.H[i, j]);
                }
                for (int j = 0, k = ModelParameters.OutputsNumber; j < ModelParameters.InputsNumber; j++, k++)
                {
                    trial *= Math.Pow(inputs[j], ModelParameters.ModelParameters.H[i, k]);
                }

                currentState[i] -= trial;
            }

            return currentState;
        }

        public IDiscreteOutput Evaluate(IDiscreteInput input)
        {
            return rungeKuttahIntegrator.SolveEquation(input);
        }
    }
}
