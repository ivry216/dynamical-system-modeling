using System;
using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.SystemsS
{
    // TODO: this class copies LdeEvaluationParameters class
    public class SSystemEvaluationParameters : NumericalIntegrationParameters, ISSystemModelEvaluationParameters
    {
        public SSystemEvaluationParameters(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
            : base(startTime, endTime, steps, areInputsPrecalculated)
        {
        }

        public SSystemEvaluationParameters(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
            : base(startTime, endTime, stepSize, areInputsPrecalculated)
        {
        }
    }
}
