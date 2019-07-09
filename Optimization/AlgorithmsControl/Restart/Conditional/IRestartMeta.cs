using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public interface IRestartMeta<TParameters> : IAlgorithm
        where TParameters : IRestartMetaParameters
    {
        IRestartableAlgorithm Algorithm { get; set; }
    }
}
