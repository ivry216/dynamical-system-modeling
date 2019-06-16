using Optimization.Parameters.Generation;

namespace Optimization.AlgorithmsInterfaces
{
    public interface IRestartableAlgorithm : IRealIterableAlgorithm
    {
        IGenerationParameters GenerationParameters { get; set; }
    }
}
