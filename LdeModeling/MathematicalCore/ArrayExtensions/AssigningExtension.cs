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

        public static void FillWithValueInPosition(this double[] array, double value, int position, double defaultValue = 0)
        {
            // TODO: optimize?
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = defaultValue;
            }
            array[position] = value;
        }

        public static void FillWithValuesInPositions(this double[] array, double[] values, int[] positions, double defaultValue = 0)
        {
            // TODO: optimize?
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = defaultValue;
            }
            for (int i = 0; i < values.Length; i++)
            {
                array[positions[i]] = values[i];
            }
        }
    }
}
