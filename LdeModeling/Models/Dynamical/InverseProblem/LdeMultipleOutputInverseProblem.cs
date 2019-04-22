using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataSample;
using TestApp.Models.Dynamical.LinearDifferentialEquation;
using TestApp.Models.Dynamical.ModelToDataProcessing;
using TestApp.Models.Dynamical.SamplePreprocessing;
using TestApp.Optimization.Problem;

namespace TestApp.Models.Dynamical.InverseProblem
{
    public class LdeMultipleOutputInverseProblem : LdeInverseProblem
    {
        #region Fields

        private double[] _outputNormalization;

        #endregion Fields

        #region Constructor

        public LdeMultipleOutputInverseProblem(int dimension) : base(dimension)
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
                _outputNormalization[i] = sample.Data.Outputs.Select(o => o[i]).Max() - sample.Data.Outputs.Select(o => o[i]).Min();
            }
        }

        public override double CalcualteCriterion(double[] alternative)
        {
            model.ModelParameters.ModelParameters.AssignWithArray(alternative);

            double result = modelToDataProcessor.CalculateMultipleOutputCriterion(model, _outputNormalization);

            return 1 / (1 + result);
        }

        #endregion Setting
    }
}
