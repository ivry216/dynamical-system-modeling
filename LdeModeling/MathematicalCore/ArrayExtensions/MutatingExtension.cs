using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.ArrayExtensions
{
    public static class MutatingExtension
    {
        public static void AddToIndex(this double[] array, double value, int index)
        {
            array[index] = array[index] + value;
        }
    }
}
