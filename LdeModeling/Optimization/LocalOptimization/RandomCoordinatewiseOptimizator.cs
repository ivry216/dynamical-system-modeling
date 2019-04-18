using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.MathematicalCore.ArrayExtensions;
using TestApp.MathematicalCore.Randomizing;
using TestApp.Optimization.Problem;

namespace TestApp.Optimization.LocalOptimization
{
    public class RandomCoordinatewiseOptimizator : OptimizationAlgorithm<RandomCoordinatewiseOptimizatorParameters>
    {
        private double _intermediateCriterionValue;
        private double _trialCriterionValue;
        private double[] _intermediate;
        private double[] _trial;

        public override void Evaluate()
        {
            if (Parameters.Type == RandomCoordinatewiseSearchType.ChooseDirection)
            {
                // Make a trial vector and intermediate solution
                _intermediate = new double[Problem.Dimension];
                _trial = new double[Problem.Dimension];

                // Set the initial solution
                _intermediate.FillWithVector(Parameters.InitialPoint);
                _intermediateCriterionValue = Parameters.InitialPointValue;

                for (int i = 0; i < Parameters.NumberOfCoordinates; i++)
                {
                    NextIteration();
                }
            }

            BestSolution = _intermediate;
            BestValue = _intermediateCriterionValue;
        }

        protected override void NextIteration()
        {
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
        }
    }
}
