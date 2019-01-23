using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public class LinearDynamicalSystemParameters
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

        public LinearDynamicalSystemParameters(int numberOfInputs, int numberOfOutputs)
        {
            // Assign the number of inputs andoutputs
            NumberOfInputs = numberOfInputs;
            NumberOfOutputs = numberOfOutputs;
            // Initialize the parameters
            _matrixA = new double[numberOfOutputs, numberOfOutputs];
            _matrixB = new double[NumberOfOutputs, numberOfInputs];
        }

        #endregion Constructor

        #region Assignment
        
        public void AssignWithArray(double[] parameters)
        {
            // Current vector position
            int vectorPosition = 0;

            // Assign the system matrix
            for (int i = 0; i < NumberOfOutputs; i++)
            {
                for (int j = 0; j < NumberOfOutputs; j++)
                {
                    _matrixA[i, j] = parameters[vectorPosition];
                    vectorPosition++;
                }
            }

            // Assign the control matrix
            for (int i = 0; i < NumberOfOutputs; i++)
            {
                for (int j = 0; j < NumberOfInputs; j++)
                {
                    _matrixB[i, j] = parameters[vectorPosition];
                    vectorPosition++;
                }
            }
        }

        #endregion Assignment

    }
}
