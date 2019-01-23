using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical
{
    public interface IDynamicalModelEvaluationParams : IModelEvaluationParameters
    {
        double FinalTime { get; }
        double StartTime { get; }
        int Steps { get; }
        double StepSize { get; }
        bool AreInputsPrecalculated { get; }
    }
}
