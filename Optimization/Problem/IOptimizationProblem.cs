namespace Optimization.Problem
{
    interface IOptimizationProblem
    {
        bool IsConstrained { get; }
        double CalcualteCriterion(double[] alternative);
    }
}
