using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.MathematicalCore.LinearOperator;
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

        private int? partsLength;
        private double[] k1;
        private double[] k2;
        private double[] k3;
        private double[] k4;
        private double[] k21;
        private double[] k31;
        private double[] k41;
        private double[] trialVectorR0;
        private double[] trialVectorR1;
        private double[] trialVectorR2;
        private double[] trialVectorR3;
        private double[] trialVectorR4;

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

        private double[] CaclulateSystemEquation(double[] result, double[] state, double[] inputs)
        {
            return model.CalculateSystemEquation(result, state, inputs);
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
            InitializePartsOfScheme();

            for (int i = 0; i < steps; i++)
            {
                int inputsIndex = i * 2;
                // Calculate K1
                k1 = CaclulateSystemEquation(k1,currentVector, precalculatedInputs[inputsIndex]);
                // K2
                k2 = CaclulateSystemEquation(k2, VectorProcessor.Instance.CalculateLinearCombination(k21, currentVector, k1, halfStep), precalculatedInputs[inputsIndex + 1]);
                // K3
                k3 = CaclulateSystemEquation(k3, VectorProcessor.Instance.CalculateLinearCombination(k31, currentVector, k2, halfStep), precalculatedInputs[inputsIndex + 1]);
                // K4
                k4 = CaclulateSystemEquation(k4, VectorProcessor.Instance.CalculateLinearCombination(k41, currentVector, k3, stepSize), precalculatedInputs[inputsIndex + 2]);

                //
                trialVector = VectorProcessor.Instance.CalcualteSum(trialVectorR0, currentVector, // y +
                    VectorProcessor.Instance.ScaleByValue(trialVectorR1, // + step/6 * (
                        VectorProcessor.Instance.CalculateLinearCombination(trialVectorR2, // 
                            VectorProcessor.Instance.CalculateLinearCombination(trialVectorR3, k1, k2, 2), // k1 + 2*k2
                            VectorProcessor.Instance.CalculateLinearCombination(trialVectorR4, k4, k3, 2), 1), sixPartStep)); // k4 + 2*k3

                // Update soluton
                currentVector.FillWithVector(trialVector);
                // Calculate results
                results[i + 1].FillWithVector(trialVector);
                // Calculate times
                times[i + 1] = times[i] + stepSize;
            }

            return (results, times);
        }

        private void InitializePartsOfScheme()
        {
            if (partsLength.HasValue && partsLength.Value == numberOfOutputs)
                return;

            partsLength = numberOfOutputs;

            k1 = new double[numberOfOutputs];
            k2 = new double[numberOfOutputs];
            k3 = new double[numberOfOutputs];
            k4 = new double[numberOfOutputs];
            k21 = new double[numberOfOutputs];
            k31 = new double[numberOfOutputs];
            k41 = new double[numberOfOutputs];
            trialVectorR0 = new double[numberOfOutputs];
            trialVectorR1 = new double[numberOfOutputs];
            trialVectorR2 = new double[numberOfOutputs];
            trialVectorR3 = new double[numberOfOutputs];
            trialVectorR4 = new double[numberOfOutputs];
        }

        #endregion Main Methods
    }
}
