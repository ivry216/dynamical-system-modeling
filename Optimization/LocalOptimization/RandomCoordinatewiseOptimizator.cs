using Randomizer.Randomizing;
using MathCore.Extensions.Arrays;
using Optimization.AlgorithmsInterfaces;

namespace Optimization.LocalOptimization
{
    public class RandomCoordinatewiseOptimizator : OptimizationAlgorithm<RandomCoordinatewiseOptimizatorParameters>
    {
        private double _intermediateCriterionValue;
        private double _trialCriterionValue;
        private double[] _intermediate;
        private double[] _trial;

        protected override void Initialize()
        {
            // Make a trial vector and intermediate solution
            _intermediate = new double[Problem.Dimension];
            _trial = new double[Problem.Dimension];

            // Initialize the best solution
            BestSolution = new double[Problem.Dimension];
        }

        protected override void Generate()
        {
            // Assign the best value to min
            BestValue = double.MinValue;

            // Initialize iteration
            Iteration = 0;

            // Set the initial solution
            _intermediate.FillWithVector(Parameters.InitialPoint);
            _intermediateCriterionValue = Parameters.InitialPointValue;

            //
            TryUpdateSolution(_intermediateCriterionValue, _intermediate);
        }

        protected override void NextIteration()
        {
            // Update iteration
            Iteration++;

            // TODO: could optimize this if the next index is the same with previous
            int chooseIndex = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, Problem.Dimension);
            int chooseSign = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, 2) - 1;

            _trial.FillWithVector(_intermediate);
            _trial[chooseIndex] += chooseSign * Parameters.Step;
            // Find criterion for the trial solution
            _trialCriterionValue = Problem.CalcualteCriterion(_trial);
            if (_trialCriterionValue > _intermediateCriterionValue)
            {
                // Make steps in taht direction while there is improvement
                for (int j = 0; j < Parameters.NumberOfSteps; j++)
                {
                    _intermediate.FillWithVector(_trial);
                    _intermediateCriterionValue = _trialCriterionValue;
                    // Make another step
                    _trial[chooseIndex] += chooseSign * Parameters.Step;
                    // Find criterion for the trial solution
                    _trialCriterionValue = Problem.CalcualteCriterion(_trial);

                    // Check if the stopping condition is met
                    if (_trialCriterionValue < _intermediateCriterionValue)
                        break;
                }
            }

            //
            TryUpdateSolution(_intermediateCriterionValue, _intermediate);
        }
    }
}
