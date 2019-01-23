using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public class LdeEvaluationParameters : ILinearDifferentialEquationEvaluationParams
    {
        public double FinalTime { get; }
        public double StartTime { get; }
        public int Steps { get; }
        public double StepSize { get; }

        public bool AreInputsPrecalculated { get; }

        public LdeEvaluationParameters(double startTime, double endTime, int steps, bool areInputsPrecalculated = false)
        {
            StartTime = startTime;
            FinalTime = endTime;
            Steps = steps;
            StepSize = (endTime - startTime) / steps;
            AreInputsPrecalculated = areInputsPrecalculated;
        }

        public LdeEvaluationParameters(double startTime, double endTime, double stepSize, bool areInputsPrecalculated = false)
        {
            StartTime = startTime;
            FinalTime = endTime;
            StepSize = stepSize;
            Steps = (int)Math.Floor((endTime - startTime) / stepSize);
            AreInputsPrecalculated = areInputsPrecalculated;
        }
    }
}
