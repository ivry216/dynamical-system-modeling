using System.Linq;
using TestApp.DataSample;
using TestApp.Models.Dynamical.SystemsS;

namespace TestApp.Models.Dynamical.InverseProblem
{
    class SSystIdentificationProblem : DynamicalInverseProblem<SSystemModel>
    {
        #region Fields

        private double[] _outputNormalization;

        #endregion Fields

        #region Constructor

        public SSystIdentificationProblem(int dimension) : base(dimension)
        {
        }

        #endregion Construtor

        #region Setting

        public override void SetData(DynamicalSystemSample sample)
        {
            base.SetData(sample);

            _outputNormalization = new double[sample.Data.NumberOfOutputs];
            for (int i = 0; i < _outputNormalization.Length; i++)
            {
                _outputNormalization[i] = sample.Data.Outputs.
                    Select(o => o[i]).Max() - sample.Data.Outputs.Select(o => o[i]).Min();
            }
        }

        public override double CalcualteCriterion(double[] alternative)
        {
            // Assign values to parameters
            double[] parameters = new double[30];

            for (int i = 0; i < 6; i++)
            {
                parameters[i] = alternative[i];
            }

            parameters[7] = alternative[6];
            parameters[8] = alternative[7];
            parameters[9] = alternative[8];
            parameters[10] = alternative[9];
            parameters[15] = alternative[10];
            parameters[18] = alternative[11];
            parameters[23] = alternative[12];
            parameters[28] = alternative[13];

            model.ModelParameters.ModelParameters.AssignWithArray(parameters);

            double result = modelToDataProcessor.CalculateMultipleOutputCriterion(model, _outputNormalization);

            return 1 / (1 + result);
        }

        #endregion Setting
    }
}
