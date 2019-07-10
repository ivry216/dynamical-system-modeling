using TestApp.DataSample;
using TestApp.Models.Dynamical.LinearDifferentialEquation;
using TestApp.Models.Dynamical.ModelToDataProcessing;
using TestApp.Models.Dynamical.SamplePreprocessing;
using Optimization.Problem;
using TestApp.Models.Dynamical.DeNumericalIntegration;
using TestApp.Models.Dynamical.SystemsS;

namespace TestApp.Models.Dynamical.InverseProblem
{
    public abstract class DynamicalInverseProblem<TModel> : OptimizationProblem
        where TModel : INumericallyCalculable, new()
    {
        #region Fields

        protected SampleToDynamicalSolutionDataProcessor sampleToLdeProcessor;
        protected ModelToDataProcessor modelToDataProcessor;
        protected TModel model;

        #endregion Fields

        #region Properties

        public int NumberOfStateVars { get; set; }
        public int NumberOfInputVars { get; set; }
        public double[] InitialValue { get; private set; }

        #endregion Properties

        #region Constructor

        public DynamicalInverseProblem(int dimension) : base(dimension)
        {
            sampleToLdeProcessor = new SampleToDynamicalSolutionDataProcessor();
            modelToDataProcessor = new ModelToDataProcessor();
            model = new TModel();
        }

        #endregion Construtor

        #region Setting

        public virtual void SetData(DynamicalSystemSample sample)
        {
            sampleToLdeProcessor.Process(sample);
            modelToDataProcessor.SetData(sample);
            var integrationParameters = modelToDataProcessor.SetIntegrationSchemeBySample();
            model.NumericalIntegrationParameters = new NumericalIntegrationParameters(integrationParameters);
            InitialValue = sample.Data.InitialValue;
        }

        public void SetInputs(IDynamicalModelInput discreteDynamicalModelInput)
        {
            modelToDataProcessor.SetInputs(discreteDynamicalModelInput);
            NumberOfInputVars = discreteDynamicalModelInput.NumberOfInputs;
        }

        public void InitializeCalculation()
        {
            model.InitializeModelParameters(NumberOfStateVars, NumberOfInputVars, InitialValue);
        }

        public override abstract double CalcualteCriterion(double[] alternative);

        #endregion Setting
    }
}
