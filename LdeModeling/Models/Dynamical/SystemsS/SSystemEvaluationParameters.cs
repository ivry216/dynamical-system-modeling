using System;

namespace TestApp.Models.Dynamical.SystemsS
{
    // TODO: this class copies LdeEvaluationParameters class
    public class SSystemEvaluationParameters : ISSystemModelEvaluationParameters
    {
        public double FinalTime { get; }
        public double StartTime { get; }
        public int Steps { get; }
        public double StepSize { get; }

        public bool AreInputsPrecalculated { get; }

        public SSystemEvaluationParameters(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
        {
            StartTime = startTime;
            FinalTime = endTime;
            Steps = steps;
            StepSize = (endTime - startTime) / steps;
            AreInputsPrecalculated = areInputsPrecalculated;
        }

        public SSystemEvaluationParameters(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
        {
            StartTime = startTime;
            FinalTime = endTime;
            StepSize = stepSize;
            Steps = (int)Math.Floor((endTime - startTime) / stepSize);
            AreInputsPrecalculated = areInputsPrecalculated;
        }
    }
}
