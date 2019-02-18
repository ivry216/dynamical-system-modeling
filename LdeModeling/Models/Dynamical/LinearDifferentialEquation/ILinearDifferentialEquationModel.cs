using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public interface ILinearDifferentialEquationModel : IDynamicalModel<ILinearDifferentialEquationEvaluationParams, ILinearDifferentialEquationParameters, IDiscreteOutput, IContiniousInput>
    {
        double[] CalculateSystemEquation(double[] result, double[] state, double[] inputs);
    }
}
