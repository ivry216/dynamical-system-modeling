namespace MathCore.Extensions.Arrays
{
    public static class MutatingExtension
    {
        public static void AddToIndex(this double[] array, double value, int index)
        {
            array[index] = array[index] + value;
        }
    }
}
