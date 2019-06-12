using MathCore.Extensions.Arrays;

namespace TestApp.Models.Dynamical
{
    public class DiscreteDynamicalModelInput : IDiscreteInput
    {
        public bool IsPrecalculated { get; private set; }

        public int NumberOfInputs => Inputs[0].Length;

        public int NumberOfPoints => Inputs.Length;

        public double[][] Inputs { get; private set; }

        public double[] Times { get; }

        public DiscreteDynamicalModelInput(double[][] inputs, double[] inputTimes)
        {
            IsPrecalculated = true;

            // Initialize
            var thisDimension = inputs[0].Length;
            Inputs = new double[inputs.Length][];
            // TODO check if times and the inputs have different dimension
            Times = new double[inputs.Length];

            for (int i = 0; i < inputs.Length; i++)
            {
                Times[i] = inputTimes[i];
                Inputs[i] = new double[thisDimension];
                // Fill the inputs
                Inputs[i].FillWithVector(inputs[i]);
            }
        }
    }
}
