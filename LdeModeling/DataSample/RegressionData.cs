using MathCore.Extensions.Arrays;

namespace TestApp.DataSample
{
    class RegressionData : IRegressionData
    {
        public int NumberOfInputs { get; }

        public int NumberOfOutputs { get; }

        public double[][] Inputs { get; }

        public double[][] Outputs { get; }

        public DataType DataType => DataType.LabeledData;

        public int OutputSampleSize { get; }

        public int InputSampleSize { get; }

        public RegressionData(double[][] inputs, double[][] outputs)
        { 
            // TODO: Add all checkings and increase safety
            OutputSampleSize = outputs.Length;
            InputSampleSize = inputs.Length;

            NumberOfInputs = inputs[0].Length;
            NumberOfOutputs = outputs[0].Length;

            Inputs = new double[OutputSampleSize][];
            Outputs = new double[OutputSampleSize][];

            for (int i = 0; i < OutputSampleSize; i++)
            {
                Inputs[i] = new double[NumberOfInputs];
                Inputs[i].FillWithVector(inputs[i]);

                Outputs[i] = new double[NumberOfOutputs];
                Outputs[i].FillWithVector(outputs[i]);
            }
        }
    }
}
