using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical
{
    public interface IDynamicalModelInput : IModelInput
    {
        bool IsPrecalculated { get; }
        int NumberOfInputs { get; }
        double[][] Inputs { get; }
        double[] Times { get; }
    }
}
