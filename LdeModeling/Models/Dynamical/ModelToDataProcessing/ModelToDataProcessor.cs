using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataSample;
using TestApp.Models.Dynamical.LinearDifferentialEquation;
using TestApp.Models.Dynamical.SamplePreprocessing;

namespace TestApp.Models.Dynamical.ModelToDataProcessing
{
    public class ModelToDataProcessor
    {
        private IDiscreteInput _inputs;
        private SampleToLdeDataProcessor _samplePreprocessing;
        private int _sampleSize;
        private DynamicalSystemSample _sample;
        private int _numberOfOutputs;

        public void SetInputs(IDynamicalModelInput inputs)
        {
            _inputs = (IDiscreteInput)inputs;
        }

        public void SetData(DynamicalSystemSample sample)
        {
            _sample = sample;
            _numberOfOutputs = _sample.Data.NumberOfOutputs;
            _samplePreprocessing = new SampleToLdeDataProcessor();
            _samplePreprocessing.Process(sample);
            _sampleSize = sample.Size;
        }

        public LdeEvaluationParameters SetIntegrationSchemeBySample()
        {
            return new LdeEvaluationParameters(_sample.Data.OutputStartTime, _sample.Data.OutputEndTime, _samplePreprocessing.IntegrationStep, areInputsPrecalculated: true);
        }

        public double CalculateSingleOutputCriterion(LdeModel model)
        {
            // Calculate the model output
            var output = (DiscreteDynamicalModelOutput)model.Evaluate(_inputs);
            // 
            double sum = 0;
            var indexAndValues = _samplePreprocessing.IntegrationStepAndOutputs;
            foreach (var index in indexAndValues.Keys)
            {
                sum += Math.Abs(output.Outputs[index][0] - indexAndValues[index][0]);
            }

            return sum / _sampleSize;
        }

        public double CalculateMultipleOutputCriterion(LdeModel model)
        {
            // Calculate the model output
            var output = (DiscreteDynamicalModelOutput)model.Evaluate(_inputs);
            //
            double sum = 0;
            var indexAndValues = _samplePreprocessing.IntegrationStepAndOutputs;
            foreach (var index in indexAndValues.Keys)
            {
                for (int i = 0; i < _numberOfOutputs; i++)
                {
                    sum += Math.Abs(output.Outputs[index][i] - indexAndValues[index][i]);
                }
            }

            return sum / (_sampleSize * _numberOfOutputs);
        }

        // TODO: code repetition!!!
        public double CalculateMultipleOutputCriterion(LdeModel model, double[] normalization)
        {
            // Calculate the model output
            var output = (DiscreteDynamicalModelOutput)model.Evaluate(_inputs);
            //
            double sum = 0;
            var indexAndValues = _samplePreprocessing.IntegrationStepAndOutputs;
            foreach (var index in indexAndValues.Keys)
            {
                for (int i = 0; i < _numberOfOutputs; i++)
                {
                    sum += Math.Abs(output.Outputs[index][i] - indexAndValues[index][i]) / normalization[i];
                }
            }

            return sum / (_sampleSize * _numberOfOutputs);
        }
    }
}
