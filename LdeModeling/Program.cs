﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataSample.SampleGeneration.LinearDynamicalSystem;
using TestApp.Models.Dynamical;
using TestApp.Models.Dynamical.InverseProblem;
using TestApp.Models.Dynamical.LinearDifferentialEquation;
using TestApp.Models.Dynamical.ModelToDataProcessing;
using TestApp.Models.Dynamical.SamplePreprocessing;
using TestApp.Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm;
using TestApp.Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm.ParameterTypes;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            double startTime = 0;
            double endTime = 20;
            int numberOfSteps = 200;

            int stateDimension = 3;
            int numberOfInputs = 1;

            LdeEvaluationParameters ldeEvaluationParameters = new LdeEvaluationParameters(startTime, endTime, numberOfSteps, true);
            LdeModelParameters ldeModelParameters = new LdeModelParameters(stateDimension, numberOfInputs);
            LinearDynamicalSystemParameters linearDynamicalSystemParameters = new LinearDynamicalSystemParameters(numberOfInputs, stateDimension);

            // Set parameters
            linearDynamicalSystemParameters.AssignWithArray(new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 1 });

            // Set initial values
            double[] initialValue = new double[] { 0, 0, 0 };
            
            // Set those parameters
            ldeModelParameters.ModelParameters = linearDynamicalSystemParameters;
            ldeModelParameters.InitialState = initialValue;

            // Model
            LdeModel model = new LdeModel(ldeEvaluationParameters, ldeModelParameters);

            // Trial input
            double[][] input = new double[(numberOfSteps + 1) * 2][];
            double[] inputTimes = new double[(numberOfSteps + 1) * 2];
            for (int i = 0; i < input.Length; i++)
            {
                inputTimes[i] = startTime + i * (endTime - startTime)/numberOfSteps/2d;
                input[i] = new double[1];
                input[i][0] = 1;
            }

            // Make input
            DiscreteDynamicalModelInput discreteDynamicalModelInput = new DiscreteDynamicalModelInput(input, inputTimes);

            var output = model.Evaluate((IDiscreteInput)discreteDynamicalModelInput);

            LdsSampleGenerator sampleGenerator = new LdsSampleGenerator();
            sampleGenerator.SetModelAndInput(model, discreteDynamicalModelInput);
            sampleGenerator.SampleSize = 100;

            var sample = sampleGenerator.Generate();

            SampleToLdeDataProcessor sampleToLdeProcessor = new SampleToLdeDataProcessor();
            sampleToLdeProcessor.Process(sample);

            ModelToDataProcessor modelToDataProcessor = new ModelToDataProcessor();
            modelToDataProcessor.SetInputs(discreteDynamicalModelInput);
            modelToDataProcessor.SetData(sample);
            var integrationScheme = modelToDataProcessor.SetIntegrationSchemeBySample();
            model.EvaluationParameters = integrationScheme;
            model.ModelParameters.InitialState = sample.Data.InitialValue;
            double result = modelToDataProcessor.CalculateCriterion(model);

            LdeInverseProblem ldeInverseProblem = new LdeInverseProblem(12);
            ldeInverseProblem.NumberOfStateVars = 3;
            ldeInverseProblem.NumberOfInputVars = 1;
            ldeInverseProblem.SetData(sample);
            ldeInverseProblem.InitializeCalculation();
            ldeInverseProblem.SetInputs(discreteDynamicalModelInput);

            ldeInverseProblem.CalcualteCriterion(new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 1 });

            RealGeneticAlgorithm realGa = new RealGeneticAlgorithm();
            RealGeneticAlgorithmParameters realGaParameters = new RealGeneticAlgorithmParameters();

            realGaParameters.GenerationType = RvgaGenerationType.Uniform;
            realGaParameters.GenerationFrom = Enumerable.Repeat(-3.0, 12).ToArray();
            realGaParameters.GenerationTo = Enumerable.Repeat(3.0, 12).ToArray();

            realGaParameters.SelectionType = RvgaSelectionType.Proportional;
            realGaParameters.NumberOfParents = 2;

            realGaParameters.CrossoverType = RvgaCrossoverType.Intermediate;

            realGaParameters.MutationType = RvgaMutationType.Uniform;
            realGaParameters.MutateFrom = Enumerable.Repeat(-3.0, 12).ToArray();
            realGaParameters.MutateTo = Enumerable.Repeat(3.0, 12).ToArray();
            realGaParameters.MutationNumberOfGenes = 2;

            realGaParameters.NextPopulationType = RvgaNextPopulationType.ParentsAndOffsprings;
            realGaParameters.SizeOfTrialPopulation = 300;
            realGaParameters.Size = 200;
            realGaParameters.Iterations = 1000;
            

            realGa.SetProblem(ldeInverseProblem);
            realGa.SetParameters(realGaParameters);
            realGa.Evaluate();

        }
    }
}
