using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public class LdeEvaluationParameters : DynamicalModelEvaluationParams, ILinearDifferentialEquationEvaluationParams
    {
        public LdeEvaluationParameters()
        { }

        public LdeEvaluationParameters(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
            : base(startTime, endTime, steps, areInputsPrecalculated)
        {
        }

        public LdeEvaluationParameters(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
            : base(startTime, endTime, stepSize, areInputsPrecalculated)
        {
        }

        public LdeEvaluationParameters(NumericalIntegrationParameters parameters)
            : this(parameters.StartTime, parameters.FinalTime, parameters.Steps, parameters.AreInputsPrecalculated)
        {
        }
    }
}
