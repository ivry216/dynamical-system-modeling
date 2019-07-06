using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical
{
    public interface INumericallyCalculable
    {
        IDynamicalModelEvaluationParams EvaluationParameters { get; }
        IDynamicalModelParameters ModelParameters { get; }

        double[] CalculateSystemEquation(double[] state, double[] inputs);

        IDiscreteOutput Evaluate(IContiniousInput input);
        IDiscreteOutput Evaluate(IDiscreteInput input);
    }
}
