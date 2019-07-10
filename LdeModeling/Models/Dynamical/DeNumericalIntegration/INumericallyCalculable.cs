using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.DeNumericalIntegration
{
    public interface INumericallyCalculable
    {
        double[] CalculateSystemEquation(double[] state, double[] inputs);

        IDiscreteOutput Evaluate(IContiniousInput input);
        IDiscreteOutput Evaluate(IDiscreteInput input);

        INumericalIntegrationParameters NumericalIntegrationParameters { get; set; }

        double[] InitialState { get; }
        int InputsNumber { get; }
        int OutputsNumber { get; }

        void InitializeModelParameters(int numberOfOutputs, int numberOfInputs, double[] initialState);
    }
}
