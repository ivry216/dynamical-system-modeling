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
    public class LdeInverseProblem : OptimizationProblem
    {
        #region Fields

        private SampleToLdeDataProcessor _sampleToLdeProcessor;
        private ModelToDataProcessor _modelToDataProcessor;
        private LdeModel _model;
        private LdeEvaluationParameters _ldeEvaluationParameters;

        #endregion Fields

        #region Properties

        public int NumberOfStateVars { get; set; }
        public int NumberOfInputVars { get; set; }
        public double[] InitialValue { get; private set; }

        #endregion Properties

        #region Constructor

        public LdeInverseProblem(int dimension) : base(dimension)
        {
            _sampleToLdeProcessor = new SampleToLdeDataProcessor();
            _modelToDataProcessor = new ModelToDataProcessor();
            _model = new LdeModel();
        }

        #endregion Construtor

        #region Setting

        public void SetData(DynamicalSystemSample sample)
        {
            _sampleToLdeProcessor.Process(sample);
            _modelToDataProcessor.SetData(sample);
            var ldeEvaluationParameters = _modelToDataProcessor.SetIntegrationSchemeBySample();
            _model.EvaluationParameters = ldeEvaluationParameters;
            InitialValue = sample.Data.InitialValue;
        }

        public void SetInputs(IDynamicalModelInput discreteDynamicalModelInput)
        {
            _modelToDataProcessor.SetInputs(discreteDynamicalModelInput);
            NumberOfInputVars = discreteDynamicalModelInput.NumberOfInputs;
        }

        public void InitializeCalculation()
        {
            _model.ModelParameters = new LdeModelParameters(NumberOfStateVars, NumberOfInputVars);
            _model.ModelParameters.InitialState = InitialValue;
            _model.ModelParameters.ModelParameters = new LinearDynamicalSystemParameters(NumberOfInputVars, NumberOfStateVars);
        }

        public override double CalcualteCriterion(double[] alternative)
        {
            _model.ModelParameters.ModelParameters.AssignWithArray(alternative);

            double result = _modelToDataProcessor.CalculateCriterion(_model);

            return 1 / (1 + result);
        }

        #endregion Setting
    }
}
