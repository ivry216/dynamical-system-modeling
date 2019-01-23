using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.Randomizing
{
    class RandomEngine
    {
        #region Fields
        private static readonly RandomEngine instance = new RandomEngine();
        private Random _random;
        #endregion Fields

        #region Static Properties
        public static RandomEngine Instance
        {
            get
            {
                return instance;
            }
        }
        #endregion Static Properties

        #region Constructor
        static RandomEngine()
        { }

        private RandomEngine()
        {
            _random = new Random();
        }
        #endregion Constructor

        #region Main Methods: Normal Distribution
        public double GenerateNormallyDistributed()
        {
            double u1 = 1.0 - _random.NextDouble();
            double u2 = 1.0 - _random.NextDouble();
            return Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
        }

        public double GenerateNormallyDistributed(double mean = 0, double sd = 1)
        {
            double u1 = 1.0 - _random.NextDouble();
            double u2 = 1.0 - _random.NextDouble();
            double randStdNormal = Math.Sqrt(-2.0 * Math.Log(u1)) * Math.Sin(2.0 * Math.PI * u2);
            return mean + sd * randStdNormal;
        }

        public double[] GenerateNormallyDistributedVector(int dimension, double mean = 0, double sd = 1)
        {
            // Initialize the vector
            double[] result = new double[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateNormallyDistributed(mean, sd);
            }

            return result;
        }

        public double[] GenerateNormallyDistributedVector(int dimension, double[] mean, double[] sd)
        {
            // Initialize the vector
            double[] result = new double[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateNormallyDistributed(mean[i], sd[i]);
            }

            return result;
        }
        #endregion Main Methods: Normal Distribution


        #region Main Methods: Uniform Distribution for Real
        public double GenerateUniformlyDistributed()
        {
            return _random.NextDouble();
        }

        public double GenerateUniformlyDistributed(double from = 0, double to = 1)
        {
            return from + (to - from) * _random.NextDouble();
        }

        public double GenerateUniformlyDistributedWDelta(double start = 0, double delta = 1)
        {
            return start + delta * _random.NextDouble();
        }

        public double[] GenerateUniformlyDistributedVector(int dimension, double from = 0, double to = 1)
        {
            // Initialize the vector
            double[] result = new double[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateUniformlyDistributed(from, to);
            }

            return result;
        }

        public double[] GenerateUniformlyDistributedVectorWDelta(int dimension, double start = 0, double delta = 1)
        {
            // Initialize the vector
            double[] result = new double[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateUniformlyDistributedWDelta(start, delta);
            }

            return result;
        }

        public double[] GenerateUniformlyDistributedVector(int dimension, double[] from, double[] to)
        {
            // Initialize the vector
            double[] result = new double[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateUniformlyDistributed(from[i], to[i]);
            }

            return result;
        }

        public double[] GenerateUniformlyDistributedVectorWDelta(int dimension, double[] start, double[] delta)
        {
            // Initialize the vector
            double[] result = new double[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateUniformlyDistributed(start[i], delta[i]);
            }

            return result;
        }
        #endregion Main Methods: Uniform Distribution for Real

        #region Main Methods: Uniform Distribution for Integers
        public int GenerateIntsUniformlyDistributed(int from, int to)
        {
            return _random.Next(from, to);
        }

        public int[] GenerateIntsUniformlyDistributedVector(int dimension, int from, int to)
        {
            // Initialize the vector
            int[] result = new int[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateIntsUniformlyDistributed(from, to);
            }

            return result;
        }

        public int[] GenerateIntsUniformlyDistributedVector(int dimension, int[] from, int[] to)
        {
            // Initialize the vector
            int[] result = new int[dimension];
            // Fill it
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateIntsUniformlyDistributed(from[i], to[i]);
            }

            return result;
        }
        #endregion Main Methods: Uniform Distribution for Integers

        #region Derivative Methods: Get Integers With Different Probabilities
        public int GenerateIndexByProbabilities(int indexesNumber, double[] probabilities)
        {
            // Generate uniformly distributed value [0 1]
            double randValue = GenerateUniformlyDistributed();

            // Find an index
            for (int i = 0; i < indexesNumber; i++)
            {
                if (randValue < probabilities[i])
                    return i;
            }

            return indexesNumber - 1;
        }

        public int[] GenerateIndexByProbabilitiesVector(int dimension, int indexesNumber, double[] probabilities)
        {
            // Initialize result
            int[] result = new int[dimension];

            // Fill all the elements
            for (int i = 0; i < dimension; i++)
            {
                result[i] = GenerateIndexByProbabilities(indexesNumber, probabilities);
            }

            return result;
        }
        #endregion Derivative Methods: Get Integers With Different Probabilities

        #region Subsetting Methods

        public int[] SubsetIndexesNoRepetition(int range, int subsetSize, bool ordered = false)
        {
            // Generate the intial indexes
            List<int> rangedIndices = Enumerable.Range(0, range).ToList();

            // Check if the generating can be satisfied
            if (subsetSize > range)
                throw new Exception("Error in subsetting method! Initial sample is more than subsetting parameter");

            // 
            int[] resultiveIndices = new int[subsetSize];
            // Fill in the list
            for (int i = 0; i < subsetSize; i++)
            {
                int generatedIndex = GenerateIntsUniformlyDistributed(0, rangedIndices.Count);

                resultiveIndices[i] = rangedIndices[generatedIndex];
                rangedIndices.RemoveAt(generatedIndex);
            }

            // If ordered
            if (ordered)
            {
                var result = resultiveIndices.ToList();
                result.Sort();
                return result.ToArray();
            }

            return resultiveIndices;
        }

        public T[] SubsetWithNoRepetitions<T>(T[] set, int subsetSize, bool ordered = false)
        {
            // Generate the intial indexes
            List<int> rangedIndices = Enumerable.Range(0, set.Length).ToList();

            // Check if the generating can be satisfied
            if (subsetSize > set.Length)
                throw new Exception("Error in subsetting method! Initial sample is more than subsetting parameter");

            // 
            int[] resultiveIndices = SubsetIndexesNoRepetition(set.Length, subsetSize, ordered);

            // Initialize the subset
            T[] subset = new T[subsetSize];
            // Fill in the subset
            for (int i = 0; i < resultiveIndices.Length; i++)
            {
                subset[i] = set[resultiveIndices[i]];
            }

            return subset;
        }

        #endregion Subsetting Methods
    }
}
