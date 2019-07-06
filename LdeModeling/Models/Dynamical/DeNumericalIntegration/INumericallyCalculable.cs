using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.DeNumericalIntegration
{
    public interface INumericallyCalculable
    {
        INumericalIntegrationParameters NumericalIntegrationParameters { get; set; }
        IDynamicalModelParameters ModelParameters { get; set; }

        double[] CalculateSystemEquation(double[] state, double[] inputs);

        IDiscreteOutput Evaluate(IContiniousInput input);
        IDiscreteOutput Evaluate(IDiscreteInput input);
    }
}
