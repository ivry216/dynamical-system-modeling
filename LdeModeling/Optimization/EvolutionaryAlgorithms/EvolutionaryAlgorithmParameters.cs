using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization.EvolutionaryAlgorithms
{
    class EvolutionaryAlgorithmParameters : OptimizationAlgorithmParameters
    {
        #region Properties
        public int Size { get; set; }
        public int Iterations { get; set; }
        #endregion Properties

        #region Constructor
        public EvolutionaryAlgorithmParameters()
        {
        }
        #endregion Constructor
    }
}
