using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.MathematicalCore.RealValuesExtensions
{
    public static class RoundingExtensions
    {
        public static bool IsCloseByAbs(this double current, double value, double delta = 0.0000000001)
        {
            return Math.Abs(current - value) < delta;
        }
    }
}
