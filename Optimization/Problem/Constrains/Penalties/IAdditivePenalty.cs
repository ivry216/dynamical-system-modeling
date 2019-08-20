namespace Optimization.Problem.Constrains.Parallel
{
    public interface IAdditivePenalty
    {
        double Evaluate(double[] alternatives);
    }
}
