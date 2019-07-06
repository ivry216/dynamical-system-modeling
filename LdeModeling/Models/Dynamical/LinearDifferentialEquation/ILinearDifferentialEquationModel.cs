using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public interface ILinearDifferentialEquationModel : IDynamicalModel<ILinearDifferentialEquationEvaluationParams, ILinearDifferentialEquationParameters, IDiscreteOutput, IContiniousInput>, INumericallyCalculable
    {
        
    }
}
