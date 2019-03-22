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
                // Initialize 
                int chooseIndex;
                int chooseSign;
                // Get the value
                _intermediateCriterionValue = Parameters.InitialPointValue;

                // Make a trial vector and intermediate solution
                _intermediate = new double[Problem.Dimension];
                _trial = new double[Problem.Dimension];

                // Set the initial solution
                _intermediate.FillWithVector(Parameters.InitialPoint);
                _intermediateCriterionValue = Parameters.InitialPointValue;

                for (int i = 0; i < Parameters.NumberOfCoordinates; i++)
                {
                    chooseIndex = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, Problem.Dimension);
                    chooseSign = RandomEngine.Instance.GenerateIntsUniformlyDistributed(0, 2) - 1;

                    _trial.FillWithVector(_intermediate);
                    _trial.AddToIndex(chooseSign * Parameters.Step, chooseIndex);
                    // Find criterion for the trial solution
                    _trialCriterionValue = Problem.CalcualteCriterion(_trial);
                    if (_trialCriterionValue > _intermediateCriterionValue)
                    {

                    }
                }

                
            }
        }

        protected override void NextIteration()
        {
            throw new NotImplementedException();
        }
    }
}
