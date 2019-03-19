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
        #region Constructor

        public LdeMultipleOutputInverseProblem(int dimension) : base(dimension)
        {
            sampleToLdeProcessor = new SampleToLdeDataProcessor();
            modelToDataProcessor = new ModelToDataProcessor();
            model = new LdeModel();
        }

        #endregion Construtor

        #region Setting

        public override double CalcualteCriterion(double[] alternative)
        {
            model.ModelParameters.ModelParameters.AssignWithArray(alternative);

            double result = modelToDataProcessor.CalculateMultipleOutputCriterion(model);

            return 1 / (1 + result);
        }

        #endregion Setting
    }
}
