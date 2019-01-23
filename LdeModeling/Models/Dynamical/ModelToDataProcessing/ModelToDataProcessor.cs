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

        public void SetInputs(IDynamicalModelInput inputs)
        {
            _inputs = (IDiscreteInput)inputs;
        }

        public void SetData(DynamicalSystemSample sample)
        {
            _sample = sample;
            _samplePreprocessing = new SampleToLdeDataProcessor();
            _samplePreprocessing.Process(sample);
            _sampleSize = sample.Size;
        }

        public LdeEvaluationParameters SetIntegrationSchemeBySample()
        {
            return new LdeEvaluationParameters(_sample.Data.OutputStartTime, _sample.Data.OutputEndTime, _samplePreprocessing.IntegrationStep, areInputsPrecalculated: true);
        }

        public double CalculateCriterion(LdeModel model)
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

            //if (sum > 0)
            //    throw new Exception();

            return sum / _sampleSize;
        }
    }
}
