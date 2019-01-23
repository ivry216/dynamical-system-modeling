using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataSample;
using TestApp.MathematicalCore.RealValuesExtensions;

namespace TestApp.Models.Dynamical.SamplePreprocessing
{
    public class SampleToLdeDataProcessor
    {
        public Dictionary<int, double[]> IntegrationStepAndOutputs { get; private set; }
        public DiscreteDynamicalModelInput DiscreteInput { get; private set; }
        public double IntegrationStep { get; private set; }

        public void Process(DynamicalSystemSample sample)
        {
            // Initialize the dictionary
            IntegrationStepAndOutputs = new Dictionary<int, double[]>();
            // Get inputs that could be used in calculation
            DiscreteInput = new DiscreteDynamicalModelInput(sample.Data.Inputs, sample.Data.InputsTimes);
            // Get integration step
            int digitsForRounding = 4;
            IntegrationStep = Math.Round(DiscreteInput.Times[2] - DiscreteInput.Times[0], digitsForRounding);
            // Assign all the timepoints for the lde integration
            double[] times = new double[(int)Math.Floor((sample.Data.OutputEndTime - sample.Data.OutputStartTime)/IntegrationStep + 1)];
            for (int i = 0; i < times.Length; i++)
            {
                times[i] = sample.Data.OutputStartTime + IntegrationStep * i;
            }
            // Get outputs indices
            var outputsTimes = sample.Data.OutputsTimes;
            var outputsValues = sample.Data.Outputs;
            bool nothingIsFound;
            for (int i = 0; i < outputsTimes.Length; i++)
            {
                nothingIsFound = true;
                for (int j = 0; j < times.Length; j++)
                {
                    if (outputsTimes[i].IsCloseByAbs(times[j]))
                    {
                        // If such a key is alredy exists
                        if (IntegrationStepAndOutputs.Keys.Contains(j))
                            throw new Exception("Finding the same index for different time points");

                        IntegrationStepAndOutputs.Add(j, outputsValues[i]);
                        nothingIsFound = false;
                        break;
                    }
                }

                if (nothingIsFound)
                {
                    throw new Exception("Nothing is found for the current time point");
                }
            }
        }
    }
}
