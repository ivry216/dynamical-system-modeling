namespace Optimization.Problem.Constrains
{
    public class HardAndSoftConstrainerParameters : IConstrainerParameters
    {
        public HardAndSoftConstrain[] Constrains { get; }

        public HardAndSoftConstrainerParameters(HardAndSoftConstrain[] constrains)
        {
            Constrains = constrains;
        }
    }
}
