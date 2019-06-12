using System;
using MathCore.Extensions.Arrays;
using MathCore.Vectors;
using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.DeNumericalIntegration
{
    class RungeKuttahIntegrator
    {
        // Model itself
        private ILinearDifferentialEquationModel model;

        // Parameters of the model
        private double[,] systemMatrix;
        private double[,] inputsMatrix;

        // The initial values
        private double[] initialValues;

        // The number of inputs and outputs
        private int numberOfInputs;
        private int numberOfOutputs;
        
        // Discrete inputs
        private double[][] discreteInputs;

        // Calculation parameters
        double finalTime;
        double startTime;
        int steps;
        double stepSize;
        double halfStep;
        double sixPartStep;

        public RungeKuttahIntegrator()
        { }

        #region Main Methods

        public void SetModel(ILinearDifferentialEquationModel equation)
        {
            model = equation;
        }

        public IDiscreteOutput SolveEquation(IDynamicalModelInput input)
        {
            // Get the evaluation parameters
            var evaluationParameters = model.EvaluationParameters;
            finalTime = evaluationParameters.FinalTime;
            startTime = evaluationParameters.StartTime;
            steps = evaluationParameters.Steps;
            stepSize = evaluationParameters.StepSize;
            halfStep = stepSize / 2;
            sixPartStep = stepSize / 6;

            // 
            if (evaluationParameters.AreInputsPrecalculated)
            {
                // TODO: put it aside from the calculating into initialization
                // Type coercing
                IDiscreteInput discreteInput = (IDiscreteInput)input;
                return SolveForPrecalculatedInputs(discreteInput);
            }
            else
            {
                // TODO: put it aside from the calculating into initialization
                // Type coercing
                IContiniousInput discreteInput = (IContiniousInput)input;
                return SolveForNotPrecalculatedInputs(discreteInput);
            }
        }

        private IDiscreteOutput SolveForPrecalculatedInputs(IDiscreteInput input)
        {
            // Parameters of the model
            var deParameters = model.ModelParameters;
            systemMatrix = deParameters.ModelParameters.A;
            inputsMatrix = deParameters.ModelParameters.B;

            // Get the initial values
            initialValues = model.ModelParameters.InitialState;

            // Get the number of inputs and outputs
            numberOfInputs = deParameters.InputsNumber;
            numberOfOutputs = deParameters.ModelParameters.NumberOfOutputs;

            // Get the input
            discreteInputs = input.Inputs;

            // Get the results
            (double[][] solution, double[] times) = ApplyRungeKuttahScheme(discreteInputs);

            // Perform an output
            DiscreteDynamicalModelOutput output = new DiscreteDynamicalModelOutput(solution, times);

            return output;
        }

        private IDiscreteOutput SolveForNotPrecalculatedInputs(IContiniousInput input)
        {
            throw new NotImplementedException();
        }

        private double[] CaclulateSystemEquation(double[] state, double[] inputs)
        {
            return model.CalculateSystemEquation(state, inputs);
        }

        private (double[][] Solution, double[] Times) ApplyRungeKuttahScheme(double[][] precalculatedInputs)
        {
            // Times
            double[] times = new double[steps + 1];

            // Resuls
            double[][] results = new double[steps + 1][];
            for (int i = 0; i < steps + 1; i++)
            {
                results[i] = new double[numberOfOutputs];
            }

            // Get the initial vector
            double[] currentVector = new double[numberOfOutputs];
            currentVector.FillWithVector(initialValues);
            results[0].FillWithVector(currentVector);
            times[0] = startTime;

            // Initialize the trial vector
            double[] trialVector;

            // Get the starting time
            double currentTime = startTime;

            // Initialize the parts of the scheme
            double[] k1 = new double[numberOfOutputs];
            double[] k2 = new double[numberOfOutputs];
            double[] k3 = new double[numberOfOutputs];
            double[] k4 = new double[numberOfOutputs];

            for (int i = 0; i < steps; i++)
            {
                int inputsIndex = i * 2;
                // Calculate K1
                k1 = CaclulateSystemEquation(currentVector, precalculatedInputs[inputsIndex]);
                // K2
                k2 = CaclulateSystemEquation(VectorOperator.CalculateLinearCombination(currentVector, k1, halfStep), precalculatedInputs[inputsIndex + 1]);
                // K3
                k3 = CaclulateSystemEquation(VectorOperator.CalculateLinearCombination(currentVector, k2, halfStep), precalculatedInputs[inputsIndex + 1]);
                // K4
                k4 = CaclulateSystemEquation(VectorOperator.CalculateLinearCombination(currentVector, k3, stepSize), precalculatedInputs[inputsIndex + 2]);

                //
                trialVector = VectorOperator.Sum(currentVector, // y +
                    VectorOperator.ScaleBy( // + step/6 * (
                        VectorOperator.CalculateLinearCombination( // 
                            VectorOperator.CalculateLinearCombination(k1, k2, 2), // k1 + 2*k2
                            VectorOperator.CalculateLinearCombination(k4, k3, 2), 1), sixPartStep)); // k4 + 2*k3

                // Update soluton
                currentVector.FillWithVector(trialVector);
                // Calculate results
                results[i + 1].FillWithVector(trialVector);
                // Calculate times
                times[i + 1] = times[i] + stepSize;
            }

            return (results, times);
        }

        #endregion Main Methods
    }
}
