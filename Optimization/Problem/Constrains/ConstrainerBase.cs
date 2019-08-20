namespace Optimization.Problem.Constrains
{
    public abstract class ConstrainerBase<TParameters> : IConstrainer
        where TParameters : IConstrainerParameters
    {
        protected TParameters _parameters;

        public ConstrainerBase(TParameters parameters)
        {
            _parameters = parameters;
        }
    }
}
