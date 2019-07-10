using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public interface IRestartMeta<TParameters> : IRealAlgorithm
        where TParameters : IRestartMetaParameters
    {
        IRestartableAlgorithm Algorithm { get; set; }
    }
}
