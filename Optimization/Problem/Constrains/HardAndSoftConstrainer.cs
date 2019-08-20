namespace Optimization.Problem.Constrains
{
    public class HardAndSoftConstrainer : ConstrainerBase<HardAndSoftConstrainerParameters>
    {
        public HardAndSoftConstrainer(HardAndSoftConstrainerParameters parameters) : base(parameters)
        {

        }

        public bool IsFeasibleForHard(double[] alternative)
        {
            for (int i = 0; i < _parameters.Constrains.Length; i++)
            {
                if (!_parameters.Constrains[i].IsFeasible(alternative[i]))
                {
                    return false;
                }
            }

            return true;
        }

        public double CalculatePenaltyForSoft(double[] alternative)
        {
            double penalty = 0;

            for (int i = 0; i < _parameters.Constrains.Length; i++)
            {
                penalty += _parameters.Constrains[i].CalculatePenalty(alternative[i]) * _parameters.Constrains[i].SoftConstrainCoeff;
            }

            return penalty;
        }
    }
}
