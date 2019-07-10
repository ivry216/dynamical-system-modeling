using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.InverseProblem
{
    public class LdeSingleOutputInverseProblem : DynamicalInverseProblem<LdeModel>
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
            model.ModelParameters.ModelParameters.B[0, 0] = 0;
            model.ModelParameters.ModelParameters.B[0, 1] = 0;
            model.ModelParameters.ModelParameters.B[0, 2] = 1;

            double result = modelToDataProcessor.CalculateSingleOutputCriterion(model);

            return 1 / (1 + result);
        }

        #endregion Setting
    }
}
