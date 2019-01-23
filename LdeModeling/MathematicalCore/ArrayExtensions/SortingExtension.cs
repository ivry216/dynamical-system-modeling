﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.ArrayExtensions
{
    public static class SortingExtension
    {
        public static int[] GetSortedIndexes(this double[] array, bool descending = false)
        {
            // Make a copy
            double[] copy = (double[])array.Clone();
            // Assign indices
            int[] indices = Enumerable.Range(0, array.Length).ToArray();
            // Sort
            Array.Sort(array, indices);

            return indices;
        }

        public static double[] GetSortedValues(this double[] array, bool descending = false)
        {
            // Make a copy
            double[] copy = (double[])array.Clone();
            // Assign indices
            int[] indices = Enumerable.Range(0, array.Length).ToArray();
            // Sort
            Array.Sort(array, indices);

            return copy;
        }

        public static (int[] Indices, double[] SortedValues) GetSortedIndicesAndValues(this double[] array, bool descending = false)
        {
            // Make a copy
            double[] copy = (double[])array.Clone();
            // Assign indices
            int[] indices = Enumerable.Range(0, array.Length).ToArray();
            // Sort
            Array.Sort(array, indices);

            return (indices, array);
        }
    }
}
