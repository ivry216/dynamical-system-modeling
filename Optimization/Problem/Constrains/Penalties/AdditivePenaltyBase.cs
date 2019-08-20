namespace Optimization.Problem.Constrains.Parallel
{
    public abstract class AdditivePenaltyBase<TParameters> : IAdditivePenalty
        where TParameters : IPenaltyParameters
    {
        protected TParameters _parameters;

        public AdditivePenaltyBase(TParameters parameters)
        {
            _parameters = parameters;
        }

        public abstract double Evaluate(double[] alternatives);
    }
}
