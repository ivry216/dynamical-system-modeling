namespace TestApp.Models.Dynamical.SystemsS
{
    public class CascadedPathawaySystemParameters
    {
        #region Fields

        private int _numberOfInputs;
        private int _numberOfOutputs;

        private double[] _alpha;
        private double[] _betta;

        private double[,] _gMatrix;
        private double[,] _hMatrix;

        #endregion Fields

        #region Properties

        public double[] Alpha;
        public double[] Betta;

        public double[,] G;
        public double[,] H;

        public int OutputsNumber => _numberOfOutputs;

        #endregion Properties

        #region Constructor

        public CascadedPathawaySystemParameters(int numberOfInputs, int numberOfOutputs)
        {
            _numberOfInputs = numberOfInputs;
            _numberOfOutputs = numberOfOutputs;

            _alpha = new double[numberOfOutputs];
            _betta = new double[numberOfOutputs];

            _gMatrix = new double[numberOfInputs + numberOfOutputs, numberOfInputs + numberOfOutputs];
        }

        #endregion Constructor
    }
}
