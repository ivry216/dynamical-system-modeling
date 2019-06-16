using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure;
using Optimization.AlgorithmsInterfaces.Parallel;
using Optimization.Problem.Parallel;

namespace Optimization.EvolutionaryAlgorithms.Parallel
{
    public abstract class ParallelEvolutionaryAlgorithm<TAlgorithmParameters, TValues, TAlternatives, TCalculationResult, TAlternativeRepresentations> : ParallelOptimizationAlgorithm<TAlgorithmParameters, TValues, TAlternatives, TCalculationResult, TAlternativeRepresentations>, IBestAlternativeAndValueGetter
        where TAlgorithmParameters : EvolutionaryAlgorithmParameters
        where TValues : IParallelOptimizationProblemValues
        where TAlternatives : IParallelOptimizationProblemAlternative
    {
        #region Fields

        protected double[][] Population;
        protected double[] Fitness;

        protected double[][] TrialPopulation;
        protected double[] TrialFitness;

        protected double[][] MergedPopulation;
        protected double[] MergedFitness;

        #endregion Fields

        #region Iteration Properties

        protected override double[][] IterationAlternatives => Population;
        protected override double[] IterationValues => Fitness;

        #endregion Iteration Properties

        #region Universal Methods

        protected override void Initialize()
        {
            // Update best values and solutions
            BestValue = 0;
            BestSolution = new double[Problem.Dimension];

            // Initialize the population
            Population = new double[Parameters.Size][];
            for (int i = 0; i < Problem.Dimension; i++)
            {
                Population[i] = new double[Problem.Dimension];
            }

            // Initialize the fitness
            Fitness = new double[Parameters.Size];
        }

        IBestVariableAndValueStats IBestAlternativeAndValueGetter.GetBestAlternativeAndValue()
        {
            return new BestVariableAndValueStats(BestValue, BestSolution);
        }

        #endregion universal Methods
    }
}
