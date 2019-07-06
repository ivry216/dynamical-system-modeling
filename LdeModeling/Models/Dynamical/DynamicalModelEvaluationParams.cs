using TestApp.Models.Dynamical.DeNumericalIntegration;

namespace TestApp.Models.Dynamical
{
    public class DynamicalModelEvaluationParams : IDynamicalModelEvaluationParams
    {
        protected NumericalIntegrationParameters numericalIntegrationParameters { get; set; }

        public INumericalIntegrationParameters NumericalIntegrationParameters
        {
            get => numericalIntegrationParameters;
            set
            {
                numericalIntegrationParameters = (NumericalIntegrationParameters)value;
            }
        }

        public DynamicalModelEvaluationParams()
        {

        }

        public DynamicalModelEvaluationParams(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
        {
            numericalIntegrationParameters = new NumericalIntegrationParameters(startTime, endTime, steps, areInputsPrecalculated);
        }

        public DynamicalModelEvaluationParams(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
        {
            numericalIntegrationParameters = new NumericalIntegrationParameters(startTime, endTime, stepSize, areInputsPrecalculated);
        }

        public DynamicalModelEvaluationParams(NumericalIntegrationParameters parameters)
            : this(parameters.StartTime, parameters.FinalTime, parameters.Steps, parameters.AreInputsPrecalculated)
        {
        }
    }
}
