using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.Normalization
{
    class Normalizer
    {
        #region Fields
        private static readonly Normalizer _singleton = new Normalizer();
        #endregion Fields

        #region Static Properties
        public static Normalizer Instance
        {
            get
            {
                return _singleton;
            }
        }
        #endregion Static Properties

        #region Constructor
        static Normalizer()
        { }

        private Normalizer()
        { }
        #endregion Constructor

        #region MinMax Normalization
        public double[] MinMaxNormalize(double[] vector)
        {
            // Normalized vector
            double[] normalized = new double[vector.Length];

            // Initialize the min and max values for search
            double minValue = double.MaxValue;
            double maxValue = double.MinValue;

            // Find min and max
            for (int i = 0; i < vector.Length; i++)
            {
                if (minValue > vector[i])
                    minValue = vector[i];

                if (maxValue < vector[i])
                    maxValue = vector[i];
            }

            // Calculate variance
            double variance = maxValue - minValue;

            // Normalize
            for (int i = 0; i < vector.Length; i++)
            {
                normalized[i] = (vector[i] - minValue) / variance;
            }

            return normalized;
        }
        #endregion MinMax Normalization

        #region Probability Converting
        public double[] ConvertoToProbabilities(double[] vector)
        {
            // Normalized vector
            double[] probabilities = new double[vector.Length];

            // Calculate sum
            double sum = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                sum += vector[i];
            }

            // Convert to probabilities
            for (int i = 0; i < vector.Length; i++)
            {
                probabilities[i] = vector[i] / sum;
            }

            return probabilities;
        }

        public double[] ConvertToCumulative(double[] vector, bool isConverted = false)
        {
            // Initialize (it contains 0 as start and 1 as end)
            double[] result = new double[vector.Length + 1];

            double[] vectorToConvert = vector;
            // If hte vector was not previously converted
            if (!isConverted)
            {
                vectorToConvert = ConvertoToProbabilities(vector);
            }

            // Make a cumulative vector
            result[0] = 0;
            for (int i = 0; i < vectorToConvert.Length; i++)
            {
                result[i + 1] = result[i] + vectorToConvert[i];
            }

            return result;
        }

        public double[] ConvertToCumulative(int[] vector, bool isConverted = false)
        {
            // Initialize (it contains 0 as start and 1 as end)
            double[] result = new double[vector.Length + 1];

            double[] vectorToConvert = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                vectorToConvert[i] = vector[i];
            }

            // If hte vector was not previously converted
            if (!isConverted)
            {
                vectorToConvert = ConvertoToProbabilities(vectorToConvert);
            }

            // Make a cumulative vector
            result[0] = 0;
            for (int i = 0; i < vectorToConvert.Length; i++)
            {
                result[i + 1] = result[i] + vector[i];
            }

            return result;
        }
        #endregion Probability Converting


    }
}
