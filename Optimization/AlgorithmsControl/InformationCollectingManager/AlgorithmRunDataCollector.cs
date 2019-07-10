using Optimization.AlgorithmsInterfaces;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.InformationCollectingManager
{
    public abstract class AlgorithmRunDataCollector<TStats> : IAlgorithmRunDataCollector<TStats>
        where TStats : IAlgorithmStats
    {
        #region Fields

        private List<TStats> _stats;

        #endregion Fields

        #region Constructor

        public AlgorithmRunDataCollector()
        {
            _stats = new List<TStats>();
        }

        #endregion Constructor

        #region Inherited Methods

        public void Add(TStats stats)
        {
            _stats.Add(stats);
            ActWhenAdd(stats);
        }

        // TODO: make it returning a copy
        public ICollection<TStats> GetAll()
        {
            return _stats;
        }

        public TStats GetByIdenx(int index)
        {
            return _stats[index];
        }

        #endregion Inherited Methods

        #region Virtual Methods

        protected virtual void ActWhenAdd(TStats statsToAdd)
        {

        }

        #endregion Virtual Methods
    }
}
