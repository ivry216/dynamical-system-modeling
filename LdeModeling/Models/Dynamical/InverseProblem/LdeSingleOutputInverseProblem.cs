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
        #region Fields

        private SampleToLdeDataProcessor _sampleToLdeProcessor;
        private ModelToDataProcessor _modelToDataProcessor;
        private LdeModel _model;
        private LdeEvaluationParameters _ldeEvaluationParameters;

        #endregion Fields

        #region Constructor

        public LdeSingleOutputInverseProblem(int dimension) : base(dimension)
        {
            _sampleToLdeProcessor = new SampleToLdeDataProcessor();
            _modelToDataProcessor = new ModelToDataProcessor();
            _model = new LdeModel();
        }

        #endregion Construtor

        #region Setting

        public override double CalcualteCriterion(double[] alternative)
        {
            // TODO Optimize this
            _model.ModelParameters.ModelParameters.AssignWithArray(alternative);
            _model.ModelParameters.ModelParameters.B[0, 0] = 0;
            _model.ModelParameters.ModelParameters.B[0, 1] = 0;
            _model.ModelParameters.ModelParameters.B[0, 2] = 1;

            double result = _modelToDataProcessor.CalculateSingleOutputCriterion(_model);

            return 1 / (1 + result);
        }

        #endregion Setting
    }
}
