using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation.SingleOutput
{
    public interface ISingleOutputLdeModel : IDynamicalModel<ISingleOutputLdeEvaluationParameters, ISingleOutputLdeModelParameters, IDiscreteOutput, IContiniousInput>, INumericallyCalculable
    {
    }
}