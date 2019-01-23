using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataSample
{
    public interface IDynamicalRegressionData : IData
    {
        double[] OutputsTimes { get; }

        int NumberOfOutputs { get; }
        double[][] Outputs { get; }

        double[] InputsTimes { get; }

        int NumberOfInputs { get; }
        double[][] Inputs { get; }

        double OutputStartTime { get; }
        double OutputEndTime { get; }

        double InputStartTime { get; }
        double InputEndTime { get; }

        double[] InitialValue { get; }
    }
}
