namespace Optimization.Problem.Constrains.Parallel
{
    interface IDeathPenalty
    {
        bool IsFeasible(double[] alternative);
    }
}
