using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.ArrayExtensions
{
    public static class AssigningExtension
    {
        public static void AssignWIntegerSequence(this int[] array, int start, int step, bool decreasing = false)
        {
            // Start assignment
            // If we need decreasing assignment
            if (decreasing)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = start;
                    start -= step;
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = start;
                    start += step;
                }
            }
        }

        public static void AssignWIntegerSequence(this double[] array, double start, double step, bool decreasing = false)
        {
            // Start assignment
            // If we need decreasing assignment
            if (decreasing)
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = start;
                    start -= step;
                }
            }
            else
            {
                for (int i = 0; i < array.Length; i++)
                {
                    array[i] = start;
                    start += step;
                }
            }
        }

        public static void FillWithVector(this double[] array, double[] anotherArray)
        {
            for (int i = 0; i < anotherArray.Length; i++)
            {
                array[i] = anotherArray[i];
            }
        }
    }
}
