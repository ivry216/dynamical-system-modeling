using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.SystemsS
{
    public interface ISSystemModel : IDynamicalModel<ISSystemModelEvaluationParameters, ISSystemModelParameters, IDiscreteOutput, IContiniousInput>, INumericallyCalculable
    { }
}
