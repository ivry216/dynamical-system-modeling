using Optimization.Problem.Constrains.Parallel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Optimization.Problem.Parallel.Constrained
{
    public abstract class ParallelOptimizationConstrainedProblem<TValues, TAlternatives> : IParallelOptimizationProblem<TValues, TAlternatives>
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        #region Fields

        protected IParallelConstrainer _constrainer;

        #endregion Fields

        #region Properties

        public int Dimension { get; }
        public bool IsConstrained => true;

        #endregion Properties

        #region Constructor

        public ParallelOptimizationConstrainedProblem(int dimension, IParallelConstrainer constrainer)
        {
            Dimension = dimension;
            _constrainer = constrainer;
        }

        #endregion Construtor

        #region Inherited Methods

        public abstract TValues CalculateCriterion(TAlternatives alternatives);

        #endregion Inherited Methods
    }
}
