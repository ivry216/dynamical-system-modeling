using TestApp.Models.Dynamical.DeNumericalIntegration;
using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.Dynamical.SystemsS
{
    // TODO: class is partly the same with LdeModel
    public class SSystemModel : ISSystemModel
    {
        private RungeKuttahIntegrator rungeKuttahIntegrator;

        public ISSystemModelEvaluationParameters EvaluationParameters { get; set; }

        public ISSystemModelParameters ModelParameters { get; set; }

        IModelEvaluationParameters IModel.EvaluationParameters => EvaluationParameters;

        IModelParameters IModel.ModelParameters => ModelParameters;

        IDynamicalModelEvaluationParams INumericallyCalculable.EvaluationParameters => EvaluationParameters;

        IDynamicalModelParameters INumericallyCalculable.ModelParameters => ModelParameters;

        public SSystemModel()
        {
            rungeKuttahIntegrator = new RungeKuttahIntegrator();
            rungeKuttahIntegrator.SetModel(this);
        }

        public IDiscreteOutput Evaluate(IContiniousInput input)
        {
            throw new System.NotImplementedException();
        }

        public IModelOutput Evaluate(IModelInput input)
        {
            throw new System.NotImplementedException();
        }

        public double[] CalculateSystemEquation(double[] state, double[] inputs)
        {
            throw new System.NotImplementedException();
        }

        public IDiscreteOutput Evaluate(IDiscreteInput input)
        {
            throw new System.NotImplementedException();
        }
    }
}
