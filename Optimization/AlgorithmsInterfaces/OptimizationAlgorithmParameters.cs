using Optimization.Parameters.Generation;

namespace Optimization.AlgorithmsInterfaces
{
    public abstract class OptimizationAlgorithmParameters : IOptimizationAlgorithmParameters
    {
        public int Iterations { get; set; }
        public IGenerationParameters GenerationParameters { get; set; }

        #region Constructor

        public OptimizationAlgorithmParameters()
        {
            GenerationParameters = new GenerationParameters();
        }

        #endregion Constructor
    }
}
