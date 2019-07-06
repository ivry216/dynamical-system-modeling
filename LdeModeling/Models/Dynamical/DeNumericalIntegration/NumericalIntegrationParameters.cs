using System;

namespace TestApp.Models.Dynamical.DeNumericalIntegration
{
    public class NumericalIntegrationParameters : INumericalIntegrationParameters
    {
        public double FinalTime { get; }
        public double StartTime { get; }
        public int Steps { get; }
        public double StepSize { get; }

        public bool AreInputsPrecalculated { get; }

        public NumericalIntegrationParameters(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
        {
            StartTime = startTime;
            FinalTime = endTime;
            Steps = steps;
            StepSize = (endTime - startTime) / steps;
            AreInputsPrecalculated = areInputsPrecalculated;
        }

        public NumericalIntegrationParameters(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
        {
            StartTime = startTime;
            FinalTime = endTime;
            StepSize = stepSize;
            Steps = (int)Math.Floor((endTime - startTime) / stepSize);
            AreInputsPrecalculated = areInputsPrecalculated;
        }

        public NumericalIntegrationParameters(NumericalIntegrationParameters parameters)
            : this(parameters.StartTime, parameters.FinalTime, parameters.Steps, parameters.AreInputsPrecalculated)
        {
        }
    }
}
