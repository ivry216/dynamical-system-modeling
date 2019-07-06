using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical
{
    public interface IDynamicalModelParameters : IModelParameters
    {
        int StateDimension { get; }
        double[] InitialState { get; set; }
        object ModelParameters { get; set; }
        int InputsNumber { get; }
        int OutputsNumber { get; }
    }
}
