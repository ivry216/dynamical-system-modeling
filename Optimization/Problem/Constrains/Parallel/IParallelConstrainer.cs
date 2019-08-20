namespace Optimization.Problem.Constrains.Parallel
{
    public interface IParallelConstrainer
    {
        double Evaluate(double[] alternatives);
    }
}
