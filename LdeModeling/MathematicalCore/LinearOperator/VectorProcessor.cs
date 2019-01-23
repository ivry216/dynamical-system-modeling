using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.LinearOperator
{
    class VectorProcessor
    {
        #region Fields
        private static readonly VectorProcessor instance = new VectorProcessor();
        #endregion Fields

        #region Static Properties
        public static VectorProcessor Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion Static Properties

        #region Constructor
        static VectorProcessor()
        { }

        private VectorProcessor()
        { }
        #endregion Constructor

        #region Main Methods
        // TODO dimensionally unsafe
        public double[] CalcualteSum(double[] vectorA, double[] vectorB)
        {
            // Initialize
            double[] result = new double[vectorA.Length];

            // Make a sum
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = vectorA[i] + vectorB[i];
            }

            return result;
        }

        public double[] CalcualteSum(double[][] vectors)
        {
            // Initialize
            double[] result = new double[vectors[0].Length];

            // Make a sum
            for (int i = 0; i < result.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < vectors.Length; j++)
                {
                    sum += vectors[j][i];
                }
                result[i] = sum;
            }

            return result;
        }

        public double[] CalculateLinearCombination(double[] vectorA, double[] vectorB, double coefB, double coefA = 1)
        {
            // Initialize
            double[] result = new double[vectorA.Length];

            // Make a weighted sum
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = coefA * vectorA[i] + coefB * vectorB[i];
            }

            return result;
        }

        public double[] ScaleByValue(double[] vector, double value)
        {
            // Initialize
            double[] result = new double[vector.Length];

            // Make a scaleble vector
            for (int i = 0; i < result.Length; i++)
            {
                result[i] = value * vector[i];
            }

            return result;
        }

        public double[] CalculateWeightedSum(double[] vectorA, double[] vectorB, double[] weightsA, double[] weightsB)
        {
            // Initialize
            double[] result = new double[vectorA.Length];

            // Make a weighted sum
            for (int i = 0; i < result.Length; i++)
            {
                double weightedSum = weightsA[i] * vectorA[i] + weightsB[i] * vectorB[i];
                double weightsSum = weightsA[i] + weightsB[i];
                result[i] = weightedSum / weightsSum;
            }

            return result;
        }

        public double[] ClaculateWeightedSum(double[][] vectors, double[] weights)
        {
            // Initialize
            double[] result = new double[vectors[0].Length];

            // Calculate sum of weights
            double sumOfWeights = 0;
            for (int i = 0; i < weights.Length; i++)
            {
                sumOfWeights += weights[i];
            }

            // Make weighed sum
            for (int i = 0; i < vectors.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < vectors[0].Length; j++)
                {
                    sum += vectors[i][j] * weights[j];
                }
                result[i] = sum / sumOfWeights;
            }

            return result;
        }

        public double[] ClaculateWeightedSum(double[][] vectors, double[][] weights)
        {
            // Initialize
            double[] result = new double[vectors[0].Length];

            // Calculate sum of weights
            double[] sumOfWeights = new double[weights.Length];
            for (int i = 0; i < weights.Length; i++)
            {
                for (int j = 0; j < weights[0].Length; j++)
                {
                    sumOfWeights[i] += weights[i][j];
                }
            }

            // Make weighed sum
            for (int i = 0; i < vectors.Length; i++)
            {
                double sum = 0;
                for (int j = 0; j < vectors[0].Length; j++)
                {
                    sum += vectors[i][j] * weights[i][j];
                }
                result[i] = sum / sumOfWeights[i];
            }

            return result;
        }
        #endregion Main Methods
    }
}
