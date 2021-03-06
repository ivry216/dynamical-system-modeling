﻿using Randomizer.Randomizing;
using System.Linq;
using TestApp.Models.Dynamical;
using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.DataSample.SampleGeneration.LinearDynamicalSystem
{
    public class DynamicalSystemDataGenerator : ISampleGenerator<DynamicalSystemSample>
    {
        private DistributionType _distributionType;

        private double _normalVariance;

        private INumericallyCalculable _model;
        private IDiscreteInput _discreteInput;

        public int SampleSize { get; set; }

        public void SetNoise(double normalDistributionVariance)
        {
            _distributionType = DistributionType.Normal;
            _normalVariance = normalDistributionVariance;
        }

        public void SetModelAndInput(INumericallyCalculable ldeModel, IDiscreteInput discreteInput)
        {
            _model = ldeModel;
            _discreteInput = discreteInput;
        }

        public DynamicalSystemSample Generate()
        {
            // Calculate the model output
            IDiscreteOutput modelOutput = _model.Evaluate(_discreteInput);
            // Get indices for sampling
            int[] indicesForSampling = RandomEngine.Instance.SubsetIndexesNoRepetition(modelOutput.Count, SampleSize, ordered: true);
            // First index
            int minIndex = indicesForSampling.Min();

            // Initialize the variables
            double[] times = new double[SampleSize];
            double[][] outputs = new double[SampleSize][];
            for (int i = 0; i < SampleSize; i++)
            {
                outputs[i] = new double[modelOutput.NumberOfOutputs];
            }

            // Assign with values
            for (int i = 0; i < SampleSize; i++)
            {
                var index = indicesForSampling[i];

                times[i] = modelOutput.Times[index];
                for (int j = 0; j < modelOutput.NumberOfOutputs; j++)
                {
                    outputs[i][j] = modelOutput.Outputs[index][j];
                }
            }

            DynamicalRegressionData data = new DynamicalRegressionData(times, outputs, _discreteInput.Times, _discreteInput.Inputs);
            DynamicalSystemSample sample = new DynamicalSystemSample(data);

            return sample;
        }
    }
}
