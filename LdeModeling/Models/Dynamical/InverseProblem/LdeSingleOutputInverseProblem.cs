using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.InverseProblem
{
    public class LdeSingleOutputInverseProblem : DynamicalInverseProblem<LdeSingleOutputModel>
    {
        #region Constructor

        public LdeSingleOutputInverseProblem(int dimension) : base(dimension)
        {
        }

        #endregion Construtor

        #region Setting

        public override double CalcualteCriterion(double[] alternative)
        {
            // TODO Optimize this
            model.ModelParameters.ModelParameters.AssignWithArray(alternative);

            double result = modelToDataProcessor.CalculateSingleOutputCriterion(model);

            return 1 / (1 + result);
        }

        #endregion Setting
    }
}
