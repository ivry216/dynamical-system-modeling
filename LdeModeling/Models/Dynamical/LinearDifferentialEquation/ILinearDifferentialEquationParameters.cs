using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public interface ILinearDifferentialEquationParameters : IDynamicalModelParameters
    {
        new LinearDynamicalSystemParameters ModelParameters { get; set; }
    }
}
