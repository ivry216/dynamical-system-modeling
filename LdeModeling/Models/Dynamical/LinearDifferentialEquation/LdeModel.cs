using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.MathematicalCore.LinearOperator;
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

        private double[,] systemMatrix => ModelParameters.ModelParameters.A;
        private double[,] inputsMatrix => ModelParameters.ModelParameters.B;

        private double[] systemMatrixResult;
        private double[] inputsMatrixResult;

        public LdeModel()
        {
            systemMatrixResult = new double[0];
            inputsMatrixResult = new double[0];

            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
        }

        public LdeModel(ILinearDifferentialEquationEvaluationParams evaluationParameters, ILinearDifferentialEquationParameters modelParameters)
        {
            EvaluationParameters = evaluationParameters;
            ModelParameters = modelParameters;

            systemMatrixResult = new double[0];
            inputsMatrixResult = new double[0];

            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
        }

        public double[] CalculateSystemEquation(double[] result, double[] state, double[] inputs)
        {
            if (systemMatrixResult.Length != state.Length)
                systemMatrixResult = new double[state.Length];

            if (inputsMatrixResult.Length != state.Length)
                inputsMatrixResult = new double[state.Length];

            return VectorProcessor.Instance.CalcualteSum(result, systemMatrix.MultiplyByVector(state, systemMatrixResult), inputsMatrix.MultiplyByVector(inputs, inputsMatrixResult));
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
    }
}
