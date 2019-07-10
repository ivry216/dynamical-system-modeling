using Randomizer.Randomizing;
using System.Linq;
using TestApp.DataSample.SampleGeneration.LinearDynamicalSystem;
using TestApp.DataSample.SampleIO.LinearDynamicalSystem;
using TestApp.Models.Dynamical;
using TestApp.Models.Dynamical.InverseProblem;
using TestApp.Models.Dynamical.LinearDifferentialEquation;
using TestApp.Models.Dynamical.ModelToDataProcessing;
using TestApp.Models.Dynamical.SamplePreprocessing;
using TestApp.Models.IOManagers.ModelingResults.Dynamical;
using TestApp.Models.IOManagers.Parameters.Dynamical;
using Optimization.AlgorithmsControl.IOManagers;
using Optimization.AlgorithmsControl.Restart.Static;
using Optimization.EvolutionaryAlgorithms;
using Optimization.EvolutionaryAlgorithms.DifferentialEvolutionAlgorithm;
using Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm;
using Optimization.EvolutionaryAlgorithms.RealValueGeneticAlgorithm.ParameterTypes;
using Optimization.LocalOptimization;
using TestApp.Models.Dynamical.SystemsS;
using System;
using Optimization.AlgorithmsControl.Restart.Conditional;
using TestApp.Models.Dynamical.LinearDifferentialEquation.SingleOutput;

namespace TestApp
{
    class Program
    {
        static void Main(string[] args)
        {
            BestValueBasedRestart restarter = new BestValueBasedRestart();
            BestValueBasedRestartParameters restartParameters = new BestValueBasedRestartParameters();
            restartParameters.BestSolutionRestartDistance = 0.05;
            restartParameters.IterationsTotal = 200;
            restartParameters.Threshold = 0.05;
            restartParameters.WindowSize = 15;

            restarter.Parameters = restartParameters;

            // Set dimension
            int dimension = 5;

            // Search parameters
            var variablesFrom = Enumerable.Repeat(-5.0, dimension).ToArray();
            var variablesTo = Enumerable.Repeat(5.0, dimension).ToArray();

            double startTime = 0;
            double endTime = 10;
            int numberOfSteps = 500;

            int stateDimension = 3;
            int numberOfInputs = 2;

            LdeSingleOutputModelEvaluationParameters evaluationParameters = new LdeSingleOutputModelEvaluationParameters(startTime, endTime, numberOfSteps, true);
            LdeSingleOutputModelParameters modelParameters = new LdeSingleOutputModelParameters(stateDimension, numberOfInputs);
            SingleOutputLdeParameters singleOutputSystemParams = new SingleOutputLdeParameters(numberOfInputs, stateDimension);

            // Real parameters
            double[] realParams = new double[]
            {
                -1.0, -2.0, -1.0,
                1.0, 2.0
            };

            // Set parameters
            singleOutputSystemParams.AssignWithArray(realParams);

            // Set initial values
            double[] initialValue = new double[] { 0, 0, 0 };

            // Set those parameters
            modelParameters.ModelParameters = singleOutputSystemParams;
            modelParameters.InitialState = initialValue;

            // Model
            LdeSingleOutputModel model = new LdeSingleOutputModel(evaluationParameters, modelParameters);

            // Trial input
            double[][] input = new double[(numberOfSteps + 1) * 2][];
            double[] inputTimes = new double[(numberOfSteps + 1) * 2];
            for (int i = 0; i < input.Length; i++)
            {
                inputTimes[i] = startTime + i * (endTime - startTime)/numberOfSteps/2d;
                input[i] = new double[2];
                input[i][0] = Math.Sin(inputTimes[i]);
                input[i][1] = Math.Cos(inputTimes[i]);
            }

            // Make input
            DiscreteDynamicalModelInput discreteDynamicalModelInput = new DiscreteDynamicalModelInput(input, inputTimes);

            var output = model.Evaluate(discreteDynamicalModelInput);

            DynamicalSystemDataGenerator sampleGenerator = new DynamicalSystemDataGenerator();

            sampleGenerator.SetModelAndInput(model, discreteDynamicalModelInput);
            sampleGenerator.SampleSize = numberOfSteps + 1;

            DynamicalModelResultsIOManager dynamicalModelResultsIOManager = new DynamicalModelResultsIOManager();

            var sample = sampleGenerator.Generate();

            LdsSampleManipulator ldsSampleManipulator = new LdsSampleManipulator();
            ldsSampleManipulator.Save("test.xlsx", sample);

            SampleToDynamicalSolutionDataProcessor sampleToLdeProcessor = new SampleToDynamicalSolutionDataProcessor();
            sampleToLdeProcessor.Process(sample);

            ModelToDataProcessor modelToDataProcessor = new ModelToDataProcessor();
            modelToDataProcessor.SetInputs(discreteDynamicalModelInput);
            modelToDataProcessor.SetData(sample);
            var integrationScheme = modelToDataProcessor.SetIntegrationSchemeBySample();
            model.EvaluationParameters = new LdeSingleOutputModelEvaluationParameters(integrationScheme);
            model.ModelParameters.InitialState = sample.Data.InitialValue;
            double result = modelToDataProcessor.CalculateSingleOutputCriterion(model);

            SSystIdentificationProblem sSystIdentificationProblem = new SSystIdentificationProblem(dimension);
            sSystIdentificationProblem.NumberOfStateVars = 3;
            sSystIdentificationProblem.NumberOfInputVars = 2;
            sSystIdentificationProblem.SetData(sample);
            sSystIdentificationProblem.InitializeCalculation();
            sSystIdentificationProblem.SetInputs(discreteDynamicalModelInput);

            var test = sSystIdentificationProblem.CalcualteCriterion(new double[] {
                10, 2, 3,
                5, 1.44, 7.2,
                -0.1, -0.05, 1,
                0.5,
                0.5,
                0.5,
                0.5,
                0.5
            });


            RealGeneticAlgorithmWRcs realGa = new RealGeneticAlgorithmWRcs();
            RealGeneticAlgorithmParametersWRcs realGaParameters = new RealGeneticAlgorithmParametersWRcs();

            // Generation type
            realGaParameters.GenerationType = PopulationGenerationType.Uniform;

            // Parameters for uniform generation
            realGaParameters.GenerationFrom = variablesFrom;
            realGaParameters.GenerationTo = variablesTo;
            // Parameters for normal generation
            realGaParameters.GenerationMean = RandomEngine.Instance.GenerateNormallyDistributedVector(dimension, 0, 3);
            realGaParameters.GenerationSd = Enumerable.Repeat(1.0, 12).ToArray();

            realGaParameters.SelectionType = RvgaSelectionType.Tournament;
            realGaParameters.NumberOfParents = 2;
            realGaParameters.TournamentSize = 10;

            realGaParameters.CrossoverType = RvgaCrossoverType.Uniform;

            realGaParameters.MutationType = RvgaMutationType.ProbabilisticUniform;
            realGaParameters.MutateFrom = variablesFrom;
            realGaParameters.MutateTo = variablesTo;
            //realGaParameters.MutateFrom = new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 };
            //realGaParameters.MutateTo = new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 };
            realGaParameters.MutationAdditiveSD = 2;
            realGaParameters.MutationProbability = 0.2;
            realGaParameters.MutationNumberOfGenes = 2;

            realGaParameters.NextPopulationType = RvgaNextPopulationType.ParentsAndOffsprings;
            realGaParameters.SizeOfTrialPopulation = 200;
            realGaParameters.Size = 100;
            realGaParameters.Iterations = 200;

            realGaParameters.IndividsToOptimizeLocally = 100;
            realGaParameters.LoParameters = new RandomCoordinatewiseOptimizatorParameters
            {
                NumberOfCoordinates = 20,
                NumberOfSteps = 5,
                Step = 0.1,
                Type = RandomCoordinatewiseSearchType.RandomDirection
            };

            realGa.SetProblem(sSystIdentificationProblem);
            realGa.SetParameters(realGaParameters);
            //realGa.Evaluate();

            restarter.Algorithm = realGa;

            StaticRestartLauncher launcher = new StaticRestartLauncher(new StaticRestartLaucherParameters() { Iterations = 40 });
            launcher.Algorithm = restarter;
            launcher.Run();

            StandardLauncherStatisticsIOManager launcherDataSaver = new StandardLauncherStatisticsIOManager();
            launcherDataSaver.SaveStats(launcher, "test_launch.xlsx");
        }

        private void SSystemTesting()
        {
            BestValueBasedRestart restarter = new BestValueBasedRestart();
            BestValueBasedRestartParameters restartParameters = new BestValueBasedRestartParameters();
            restartParameters.BestSolutionRestartDistance = 0.05;
            restartParameters.IterationsTotal = 200;
            restartParameters.Threshold = 0.05;
            restartParameters.WindowSize = 15;

            restarter.Parameters = restartParameters;

            // Set dimension
            int dimension = 14;

            // Search parameters
            var variablesFrom = Enumerable.Repeat(0.0, 6).Concat(Enumerable.Repeat(-4.0, dimension - 6)).ToArray();
            var variablesTo = Enumerable.Repeat(20.0, 6).Concat(Enumerable.Repeat(4.0, dimension - 6)).ToArray();

            double startTime = 0;
            double endTime = 10;
            int numberOfSteps = 500;

            int stateDimension = 3;
            int numberOfInputs = 1;

            SSystemEvaluationParameters sEvaluationParameters = new SSystemEvaluationParameters(startTime, endTime, numberOfSteps, true);
            SSystemModelParameters sModelParameters = new SSystemModelParameters(stateDimension, numberOfInputs);
            CascadedPathawaySystemParameters cascadeSystemParams = new CascadedPathawaySystemParameters(numberOfInputs, stateDimension);

            // Real parameters
            double[] realParameters = new double[] {
                10, 2, 3,
                5, 1.44, 7.2,
                0, -0.1, -0.05, 1,
                0.5, 0, 0, 0,
                0, 0.5, 0, 0,
                0.5, 0, 0, 0,
                0, 0.5, 0, 0,
                0, 0, 0.5, 0
            };

            // Set parameters
            cascadeSystemParams.AssignWithArray(realParameters);

            // Set initial values
            double[] initialValue = new double[] { 0.4, 0.4, 0.4 };

            // Set those parameters
            sModelParameters.ModelParameters = cascadeSystemParams;
            sModelParameters.InitialState = initialValue;

            // Model
            SSystemModel model = new SSystemModel(sEvaluationParameters, sModelParameters);

            // Trial input
            double[][] input = new double[(numberOfSteps + 1) * 2][];
            double[] inputTimes = new double[(numberOfSteps + 1) * 2];
            for (int i = 0; i < input.Length; i++)
            {
                inputTimes[i] = startTime + i * (endTime - startTime) / numberOfSteps / 2d;
                input[i] = new double[1];
                input[i][0] = 0.7;
            }

            // Make input
            DiscreteDynamicalModelInput discreteDynamicalModelInput = new DiscreteDynamicalModelInput(input, inputTimes);

            var output = model.Evaluate(discreteDynamicalModelInput);

            DynamicalSystemDataGenerator sampleGenerator = new DynamicalSystemDataGenerator();

            sampleGenerator.SetModelAndInput(model, discreteDynamicalModelInput);
            sampleGenerator.SampleSize = numberOfSteps + 1;

            DynamicalModelResultsIOManager dynamicalModelResultsIOManager = new DynamicalModelResultsIOManager();

            var sample = sampleGenerator.Generate();

            LdsSampleManipulator ldsSampleManipulator = new LdsSampleManipulator();
            ldsSampleManipulator.Save("test.xlsx", sample);

            SampleToDynamicalSolutionDataProcessor sampleToLdeProcessor = new SampleToDynamicalSolutionDataProcessor();
            sampleToLdeProcessor.Process(sample);

            ModelToDataProcessor modelToDataProcessor = new ModelToDataProcessor();
            modelToDataProcessor.SetInputs(discreteDynamicalModelInput);
            modelToDataProcessor.SetData(sample);
            var integrationScheme = modelToDataProcessor.SetIntegrationSchemeBySample();
            model.EvaluationParameters = new SSystemEvaluationParameters(integrationScheme);
            model.ModelParameters.InitialState = sample.Data.InitialValue;
            double result = modelToDataProcessor.CalculateSingleOutputCriterion(model);

            SSystIdentificationProblem sSystIdentificationProblem = new SSystIdentificationProblem(dimension);
            sSystIdentificationProblem.NumberOfStateVars = 3;
            sSystIdentificationProblem.NumberOfInputVars = 1;
            sSystIdentificationProblem.SetData(sample);
            sSystIdentificationProblem.InitializeCalculation();
            sSystIdentificationProblem.SetInputs(discreteDynamicalModelInput);

            var test = sSystIdentificationProblem.CalcualteCriterion(new double[] {
                10, 2, 3,
                5, 1.44, 7.2,
                -0.1, -0.05, 1,
                0.5,
                0.5,
                0.5,
                0.5,
                0.5
            });


            RealGeneticAlgorithmWRcs realGa = new RealGeneticAlgorithmWRcs();
            RealGeneticAlgorithmParametersWRcs realGaParameters = new RealGeneticAlgorithmParametersWRcs();

            // Generation type
            realGaParameters.GenerationType = PopulationGenerationType.Uniform;

            // Parameters for uniform generation
            realGaParameters.GenerationFrom = variablesFrom;
            realGaParameters.GenerationTo = variablesTo;
            // Parameters for normal generation
            realGaParameters.GenerationMean = RandomEngine.Instance.GenerateNormallyDistributedVector(dimension, 0, 3);
            realGaParameters.GenerationSd = Enumerable.Repeat(1.0, 12).ToArray();

            realGaParameters.SelectionType = RvgaSelectionType.Tournament;
            realGaParameters.NumberOfParents = 2;
            realGaParameters.TournamentSize = 10;

            realGaParameters.CrossoverType = RvgaCrossoverType.Uniform;

            realGaParameters.MutationType = RvgaMutationType.ProbabilisticUniform;
            realGaParameters.MutateFrom = variablesFrom;
            realGaParameters.MutateTo = variablesTo;
            //realGaParameters.MutateFrom = new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 };
            //realGaParameters.MutateTo = new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 };
            realGaParameters.MutationAdditiveSD = 2;
            realGaParameters.MutationProbability = 0.2;
            realGaParameters.MutationNumberOfGenes = 2;

            realGaParameters.NextPopulationType = RvgaNextPopulationType.ParentsAndOffsprings;
            realGaParameters.SizeOfTrialPopulation = 200;
            realGaParameters.Size = 100;
            realGaParameters.Iterations = 200;

            realGaParameters.IndividsToOptimizeLocally = 100;
            realGaParameters.LoParameters = new RandomCoordinatewiseOptimizatorParameters
            {
                NumberOfCoordinates = 20,
                NumberOfSteps = 5,
                Step = 0.1,
                Type = RandomCoordinatewiseSearchType.RandomDirection
            };

            realGa.SetProblem(sSystIdentificationProblem);
            realGa.SetParameters(realGaParameters);
            //realGa.Evaluate();

            restarter.Algorithm = realGa;

            StaticRestartLauncher launcher = new StaticRestartLauncher(new StaticRestartLaucherParameters() { Iterations = 40 });
            launcher.Algorithm = restarter;
            launcher.Run();

            StandardLauncherStatisticsIOManager launcherDataSaver = new StandardLauncherStatisticsIOManager();
            launcherDataSaver.SaveStats(launcher, "test_launch.xlsx");
        }

        private void PreviousTesting()
        {
            // -----------------------------------------------------------------------------------------------------
            // Eta chast koda - schityvautsia ishodnyed dannye, kotorye dalee vo vseh parallelnyh rasschetah budut
            double startTime = 0;
            double endTime = 20;
            int numberOfSteps = 200;

            int stateDimension = 3;
            int numberOfInputs = 1;

            LdeEvaluationParameters ldeEvaluationParameters = new LdeEvaluationParameters(startTime, endTime, numberOfSteps, true);
            LdeModelParameters ldeModelParameters = new LdeModelParameters(stateDimension, numberOfInputs);
            LinearDynamicalSystemParameters linearDynamicalSystemParameters = new LinearDynamicalSystemParameters(numberOfInputs, stateDimension);

            // Real parameters
            double[] realParameters = new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 };
            // Set parameters
            linearDynamicalSystemParameters.AssignWithArray(new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 });


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
                inputTimes[i] = startTime + i * (endTime - startTime) / numberOfSteps / 2d;
                input[i] = new double[1];
                input[i][0] = 1;
            }

            // Make input
            DiscreteDynamicalModelInput discreteDynamicalModelInput = new DiscreteDynamicalModelInput(input, inputTimes);

            var output = model.Evaluate(discreteDynamicalModelInput);

            DynamicalSystemDataGenerator sampleGenerator = new DynamicalSystemDataGenerator();
            sampleGenerator.SetModelAndInput(model, discreteDynamicalModelInput);
            sampleGenerator.SampleSize = 200;

            DynamicalModelResultsIOManager dynamicalModelResultsIOManager = new DynamicalModelResultsIOManager();

            var sample = sampleGenerator.Generate();

            LdsSampleManipulator ldsSampleManipulator = new LdsSampleManipulator();
            ldsSampleManipulator.Save("test.xlsx", sample);

            SampleToDynamicalSolutionDataProcessor sampleToLdeProcessor = new SampleToDynamicalSolutionDataProcessor();
            sampleToLdeProcessor.Process(sample);

            ModelToDataProcessor modelToDataProcessor = new ModelToDataProcessor();
            modelToDataProcessor.SetInputs(discreteDynamicalModelInput);
            modelToDataProcessor.SetData(sample);
            var integrationScheme = modelToDataProcessor.SetIntegrationSchemeBySample();
            model.EvaluationParameters = new LdeEvaluationParameters(integrationScheme);
            model.ModelParameters.InitialState = sample.Data.InitialValue;
            double result = modelToDataProcessor.CalculateSingleOutputCriterion(model);

            LdeMultipleOutputInverseProblem ldeInverseProblem = new LdeMultipleOutputInverseProblem(12);
            ldeInverseProblem.NumberOfStateVars = 3;
            ldeInverseProblem.NumberOfInputVars = 1;
            ldeInverseProblem.SetData(sample);
            ldeInverseProblem.InitializeCalculation();
            ldeInverseProblem.SetInputs(discreteDynamicalModelInput);

            LdeMultipleOutputInverseProblemL1 ldeInverseProblemL1 = new LdeMultipleOutputInverseProblemL1(12);
            ldeInverseProblemL1.NumberOfStateVars = 3;
            ldeInverseProblemL1.NumberOfInputVars = 1;
            ldeInverseProblemL1.SetData(sample);
            ldeInverseProblemL1.InitializeCalculation();
            ldeInverseProblemL1.SetInputs(discreteDynamicalModelInput);
            ldeInverseProblemL1.Lambda = 0.01;

            LdeMultipleOutputInverseProblemL2 ldeInverseProblemL2 = new LdeMultipleOutputInverseProblemL2(12);
            ldeInverseProblemL2.NumberOfStateVars = 3;
            ldeInverseProblemL2.NumberOfInputVars = 1;
            ldeInverseProblemL2.SetData(sample);
            ldeInverseProblemL2.InitializeCalculation();
            ldeInverseProblemL2.SetInputs(discreteDynamicalModelInput);
            ldeInverseProblemL2.Lambda = 0.01;

            var test = ldeInverseProblem.CalcualteCriterion(new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 });

            // -----------------------------------------------------------------------------------------------------
            // A vot ety chast do samogo niza nado rasparallelit
            RealGeneticAlgorithmWRcs realGa = new RealGeneticAlgorithmWRcs();
            RealGeneticAlgorithmParametersWRcs realGaParameters = new RealGeneticAlgorithmParametersWRcs();

            // Generation type
            realGaParameters.GenerationType = PopulationGenerationType.Uniform;

            // Parameters for uniform generation
            realGaParameters.GenerationFrom = Enumerable.Repeat(-3.0, 12).ToArray();
            realGaParameters.GenerationTo = Enumerable.Repeat(3.0, 12).ToArray();
            // Parameters for normal generation
            realGaParameters.GenerationMean = RandomEngine.Instance.GenerateNormallyDistributedVector(12, 0, 3);
            realGaParameters.GenerationSd = Enumerable.Repeat(1.0, 12).ToArray();

            realGaParameters.SelectionType = RvgaSelectionType.Tournament;
            realGaParameters.NumberOfParents = 2;
            realGaParameters.TournamentSize = 10;

            realGaParameters.CrossoverType = RvgaCrossoverType.Uniform;

            realGaParameters.MutationType = RvgaMutationType.ProbabilisticUniform;
            realGaParameters.MutateFrom = Enumerable.Repeat(-3.0, 12).ToArray();
            realGaParameters.MutateTo = Enumerable.Repeat(3.0, 12).ToArray();
            //realGaParameters.MutateFrom = new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 };
            //realGaParameters.MutateTo = new double[] { 0, 1, 0, 0, 0, 1, -1, -2, -1, 0, 0, 2 };
            realGaParameters.MutationAdditiveSD = 2;
            realGaParameters.MutationProbability = 0.05;
            realGaParameters.MutationNumberOfGenes = 2;

            realGaParameters.NextPopulationType = RvgaNextPopulationType.ParentsAndOffsprings;
            realGaParameters.SizeOfTrialPopulation = 200;
            realGaParameters.Size = 100;
            realGaParameters.Iterations = 40;

            realGaParameters.IndividsToOptimizeLocally = 10;
            realGaParameters.LoParameters = new RandomCoordinatewiseOptimizatorParameters
            {
                NumberOfCoordinates = 200,
                NumberOfSteps = 5,
                Step = 0.05,
                Type = RandomCoordinatewiseSearchType.RandomDirection
            };

            realGa.SetProblem(ldeInverseProblem);
            realGa.SetParameters(realGaParameters);
            realGa.Evaluate();

            DifferentialEvolution differentialEvolution = new DifferentialEvolution();
            DifferentialEvolutionParameters differentialEvolutionParameters = new DifferentialEvolutionParameters();

            differentialEvolutionParameters.CrossoverProbability = 1.2;
            differentialEvolutionParameters.DifferentialWeight = 1.2;

            differentialEvolutionParameters.GenerationParameters.GenerationType = PopulationGenerationType.Uniform;
            differentialEvolutionParameters.GenerationParameters.GenerationFrom = Enumerable.Repeat(-3.0, 12).ToArray();
            differentialEvolutionParameters.GenerationParameters.GenerationTo = Enumerable.Repeat(3.0, 12).ToArray();

            differentialEvolutionParameters.Size = 200;
            differentialEvolutionParameters.Iterations = 500;

            differentialEvolution.SetParameters(differentialEvolutionParameters);
            differentialEvolution.SetProblem(ldeInverseProblemL1);

            //differentialEvolution.Evaluate();

            model.ModelParameters.ModelParameters.AssignWithArray(realGa.BestSolution);
            DynamicalModelResultsIOManager ioResultsManager = new DynamicalModelResultsIOManager();
            ioResultsManager.Save((DiscreteDynamicalModelOutput)model.Evaluate(discreteDynamicalModelInput), "test1.xlsx");
            ioResultsManager.Save((DiscreteDynamicalModelOutput)model.Evaluate(discreteDynamicalModelInput), discreteDynamicalModelInput, "test2.xlsx");
            DynamicalModelParametersIOManager ioParamsManager = new DynamicalModelParametersIOManager();
            ioParamsManager.Save((LdeModelParameters)model.ModelParameters, "test3.xlsx");

            StaticRestartLauncher launcher = new StaticRestartLauncher(new StaticRestartLaucherParameters() { Iterations = 40 });
            launcher.Algorithm = realGa;
            launcher.Run();

            StandardLauncherStatisticsIOManager launcherDataSaver = new StandardLauncherStatisticsIOManager();
            launcherDataSaver.SaveStats(launcher, "test_launch.xlsx");

            var test1 = ldeInverseProblem.CalcualteCriterion(launcher.Algorithm.BestSolution);
        }
    }
}
