using System.Linq;
using MathCore.Extensions.Arrays;

namespace TestApp.DataSample
{
    class DynamicalRegressionData : IDynamicalRegressionData
    {
        public double[] OutputsTimes { get; }

        public int NumberOfOutputs { get; }

        public double[][] Outputs { get; }

        public double OutputStartTime { get; }

        public double OutputEndTime { get; }

        public double InputStartTime { get; }

        public double InputEndTime { get; }

        public double[] InitialValue => Outputs[0];

        public DataType DataType => DataType.LabeledData;

        public int OutputSampleSize { get; }

        public int InputSampleSize { get; }

        public double[] InputsTimes { get; }

        public int NumberOfInputs { get; }

        public double[][] Inputs { get; }

        public DynamicalRegressionData(double[] outputTimes, double[][] outputs, double[] inputTimes, double[][] inputs)
        {
            // TODO: add checking nulls and so on

            OutputSampleSize = outputTimes.Length;
            InputSampleSize = inputTimes.Length;

            OutputsTimes = new double[OutputSampleSize];
            OutputsTimes.FillWithVector(outputTimes);

            InputsTimes = new double[InputSampleSize];
            InputsTimes.FillWithVector(inputTimes);

            Outputs = new double[OutputSampleSize][];

            NumberOfOutputs = outputs[0].Length;

            for (int i = 0; i < OutputSampleSize; i++)
            {
                Outputs[i] = new double[NumberOfOutputs];
                Outputs[i].FillWithVector(outputs[i]);
            }

            Inputs = new double[InputSampleSize][];

            NumberOfInputs = inputs[0].Length;

            for (int i = 0; i < InputSampleSize; i++)
            {
                Inputs[i] = new double[NumberOfInputs];
                Inputs[i].FillWithVector(inputs[i]);
            }

            OutputStartTime = OutputsTimes.Min();
            OutputEndTime = OutputsTimes.Max();

            InputStartTime = InputsTimes.Min();
            InputEndTime = InputsTimes.Max();
        }
    }
}
