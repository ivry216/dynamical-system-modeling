using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.SystemsS
{
    public interface ISSystemModel : IDynamicalModel<ISSystemModelParameters, ISSystemModelEvaluationParameters, IDiscreteOutput, IContiniousInput>
    {
    }
}
