using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.InformationCollectingManager
{
    public interface IAlgorithmRunDataCollector<TStats>
        where TStats : IAlgorithmStats
    {
        void Add(TStats stats);
        TStats GetByIdenx(int index);
        ICollection<TStats> GetAll();
    }
}
