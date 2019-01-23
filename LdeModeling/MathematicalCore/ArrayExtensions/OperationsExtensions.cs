using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.ArrayExtensions
{
    public static class OperationsExtensions
    {
        public static double[] MultiplyByVector(this double[,] matrix, double[] vector)
        {
            // Dimension
            int numberOfCols = vector.Length;
            int numberOfRows = matrix.GetLength(0);
            // Initialize result 
            double[] result = new double[numberOfRows];

            for (int i = 0; i < numberOfRows; i++)
            {
                result[i] = 0;
                for (int j = 0; j < numberOfCols; j++)
                {
                    result[i] += matrix[i, j] * vector[j];
                }
            }

            return result;
        }
    }
}
