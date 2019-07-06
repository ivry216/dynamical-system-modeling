namespace TestApp.Models.Dynamical.SystemsS
{
    public class CascadedPathawaySystemParameters
    {
        #region Fields

        private int _numberOfInputs;
        private int _numberOfOutputs;
        private int _sumOfInputsAndOutputs;

        private double[] _alpha;
        private double[] _betta;

        private double[,] _gMatrix;
        private double[,] _hMatrix;

        #endregion Fields

        #region Properties

        public double[] Alpha => _alpha;
        public double[] Betta => _betta;

        public double[,] G => _gMatrix;
        public double[,] H => _hMatrix;

        public int OutputsNumber => _numberOfOutputs;

        #endregion Properties

        #region Constructor

        public CascadedPathawaySystemParameters(int numberOfInputs, int numberOfOutputs)
        {
            _numberOfInputs = numberOfInputs;
            _numberOfOutputs = numberOfOutputs;
            _sumOfInputsAndOutputs = _numberOfInputs + _numberOfOutputs;

            _alpha = new double[numberOfOutputs];
            _betta = new double[numberOfOutputs];

            _gMatrix = new double[numberOfOutputs, _sumOfInputsAndOutputs];
            _hMatrix = new double[numberOfOutputs, _sumOfInputsAndOutputs];
        }

        #endregion Constructor

        #region Methods

        public void AssignWithArray(double[] parameters)
        {
            // Current vector position
            int vectorPosition = 0;

            // Assign vectors
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                _alpha[i] = parameters[vectorPosition];
                vectorPosition++;
            }
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                _betta[i] = parameters[vectorPosition];
                vectorPosition++;
            }

            // Assign matrixes
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _sumOfInputsAndOutputs; j++)
                {
                    _gMatrix[i, j] = parameters[vectorPosition];
                    vectorPosition++;
                }
            }
            for (int i = 0; i < _numberOfOutputs; i++)
            {
                for (int j = 0; j < _sumOfInputsAndOutputs; j++)
                {
                    _hMatrix[i, j] = parameters[vectorPosition];
                    vectorPosition++;
                }
            }
        }

        #endregion Methods
    }
}
