using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.Problem
{
    interface IOptimizationProblem
    {
        bool IsConstrained { get; }
        double CalcualteCriterion(double[] alternative);
    }
}
