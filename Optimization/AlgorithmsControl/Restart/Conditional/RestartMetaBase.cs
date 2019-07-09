using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Optimization.AlgorithmsInterfaces;

namespace Optimization.AlgorithmsControl.Restart.Conditional
{
    public abstract class RestartMetaBase<TParameters> : IRestartMeta<TParameters>
        where TParameters : IRestartMetaParameters
    {
        protected TParameters _parameters;

        public IRestartableAlgorithm Algorithm { get; set; }

        object IAlgorithm.BestValue => throw new NotImplementedException();

        object IAlgorithm.BestSolution => throw new NotImplementedException();

        public void Evaluate()
        {
            throw new NotImplementedException();
        }

        void IAlgorithm.SetParameters(object parameters)
        {
            _parameters = (TParameters)parameters;
        }
    }
}
