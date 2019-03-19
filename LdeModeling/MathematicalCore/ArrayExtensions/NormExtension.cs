using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static System.Math;

namespace TestApp.MathematicalCore.ArrayExtensions
{
    public static class NormExtension
    {
        public static double CalculateL1Norm(this double[] vector)
        {
            double norm = 0;
            foreach (var number in vector)
            {
                norm += Abs(number);
            }
            return norm;
        }

        public static double CalculateL1Norm(this double[] vector, bool[] mask)
        {
            double norm = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                norm += mask[i] ? vector[i] : 0;
            }
            return norm;
        }

        public static double CalculateL2Norm(this double[] vector)
        {
            double norm = 0;
            foreach (var number in vector)
            {
                norm += number * number;
            }
            return norm;
        }

        public static double CalculateL2Norm(this double[] vector, bool[] mask)
        {
            double norm = 0;
            for (int i = 0; i < vector.Length; i++)
            {
                norm += mask[i] ? vector[i]*vector[i] : 0;
            }
            return norm;
        }
    }
}
