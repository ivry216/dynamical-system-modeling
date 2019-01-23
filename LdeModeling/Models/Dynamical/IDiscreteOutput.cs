using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical
{
    public interface IDiscreteOutput : IDynamicalModelOutput
    {
        int NumberOfOutputs { get; }
        int Count { get; }
        double[][] Outputs { get; }
        double[] Times { get; }
    }
}
