using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataSample
{
    interface IRegressionData : IData
    {
        int NumberOfInputs { get; }
        int NumberOfOutputs { get; }
        double[][] Inputs { get; }
        double[][] Outputs { get; }
    }
}
