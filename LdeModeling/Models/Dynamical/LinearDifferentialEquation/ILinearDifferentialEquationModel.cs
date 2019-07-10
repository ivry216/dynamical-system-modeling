using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public interface ILinearDifferentialEquationModel : IDynamicalModel<ILinearDifferentialEquationEvaluationParams, ILinearDifferentialEquationParameters, IDiscreteOutput, IContiniousInput>, INumericallyCalculable
    {
        
    }
}
