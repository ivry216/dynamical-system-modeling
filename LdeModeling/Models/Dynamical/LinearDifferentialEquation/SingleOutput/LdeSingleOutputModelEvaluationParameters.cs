using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation.SingleOutput
{
    class LdeSingleOutputModelEvaluationParameters : DynamicalModelEvaluationParams, ISingleOutputLdeEvaluationParameters
    {
        public LdeSingleOutputModelEvaluationParameters()
        { }

        public LdeSingleOutputModelEvaluationParameters(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
            : base(startTime, endTime, steps, areInputsPrecalculated)
        {
        }

        public LdeSingleOutputModelEvaluationParameters(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
            : base(startTime, endTime, stepSize, areInputsPrecalculated)
        {
        }

        public LdeSingleOutputModelEvaluationParameters(NumericalIntegrationParameters parameters)
            : this(parameters.StartTime, parameters.FinalTime, parameters.Steps, parameters.AreInputsPrecalculated)
        {
        }
    }
}
