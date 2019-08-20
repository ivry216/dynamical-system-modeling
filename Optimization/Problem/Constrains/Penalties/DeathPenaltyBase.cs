namespace Optimization.Problem.Constrains.Parallel
{
    public abstract class DeathPenaltyBase<TParameters> : IDeathPenalty
        where TParameters : IPenaltyParameters
    {
        protected TParameters _parameters;

        public DeathPenaltyBase(TParameters parameters)
        {
            _parameters = parameters;
        }

        public abstract bool IsFeasible(double[] alternative);
    }
}
