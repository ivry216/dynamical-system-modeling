using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public class LdeEvaluationParameters : NumericalIntegrationParameters, ILinearDifferentialEquationEvaluationParams
    {
        public LdeEvaluationParameters(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
            : base(startTime, endTime, steps, areInputsPrecalculated)
        {
        }

        public LdeEvaluationParameters(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
            : base(startTime, endTime, stepSize, areInputsPrecalculated)
        {
        }
    }
}
