using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation.SingleOutput
{
    // TODO: same as LinearDynamicSystemParameters - ???
    public class SingleOutputLdeParameters
    {
        #region Fields

        private double[,] _matrixA;
        private double[,] _matrixB;

        #endregion Fields

        #region Properties

        public double[,] A => _matrixA;
        public double[,] B => _matrixB;

        public int NumberOfInputs { get; }
        public int NumberOfOutputs { get; }

        #endregion Properties

        #region Constructor

        public SingleOutputLdeParameters(int numberOfInputs, int numberOfOutputs)
        {
            // Assign the number of inputs andoutputs
            NumberOfInputs = numberOfInputs;
            NumberOfOutputs = numberOfOutputs;
            // Initialize the parameters
            _matrixA = new double[numberOfOutputs, numberOfOutputs];
            _matrixB = new double[NumberOfOutputs, numberOfInputs];
            // 
            AssignInitialMatrixes();
        }

        #endregion Constructor

        #region Assignment

        private void AssignInitialMatrixes()
        {
            for (int i = 0; i < NumberOfOutputs - 1; i++)
            {
                for (int j = 0; j < NumberOfOutputs; j++)
                {
                    _matrixA[i, j] = 0;
                }

                _matrixA[i, i + 1] = 1;

                for (int j = 0; j < NumberOfInputs; j++)
                {
                    _matrixB[j, i] = 0;
                }
            }
        }

        public void AssignWithArray(double[] parameters)
        {
            int iteration = 0;

            for (int i = 0; i < NumberOfOutputs; i++)
            {
                _matrixA[NumberOfOutputs - 1, i] = parameters[iteration];
                iteration++;
            }

            for (int i = 0; i < NumberOfInputs; i++)
            {
                _matrixB[NumberOfOutputs - 1, i] = parameters[iteration];
            }
        }

        #endregion Assignment
    }
}
