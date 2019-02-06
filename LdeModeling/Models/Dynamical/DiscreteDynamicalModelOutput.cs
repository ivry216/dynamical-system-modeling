using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical
{
    public class DiscreteDynamicalModelOutput : IDiscreteOutput
    {
        public int NumberOfOutputs { get; }

        public int Count { get; }

        public double[][] Outputs { get; }

        public double[] Times { get; }

        public DiscreteDynamicalModelOutput(double[][] outputs, double[] times)
        {
            NumberOfOutputs = outputs[0].Length;
            Count = times.Length;
            Outputs = outputs;
            Times = times;
        }
    }
}
