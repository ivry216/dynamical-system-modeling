using static System.Math;

namespace MathCore.Extensions.Reals
{
    public static class RoundingExtensions
    {
        public static bool IsCloseByAbs(this double current, double value, double delta = 0.0000000001)
        {
            return Abs(current - value) < delta;
        }
    }
}
