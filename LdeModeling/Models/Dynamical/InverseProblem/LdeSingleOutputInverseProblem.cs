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
    public class LdeSingleOutputInverseProblem : LdeInverseProblem
    {
        #region Constructor

        public LdeSingleOutputInverseProblem(int dimension) : base(dimension)
        {
            sampleToLdeProcessor = new SampleToLdeDataProcessor();
            modelToDataProcessor = new ModelToDataProcessor();
            model = new LdeModel();
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
