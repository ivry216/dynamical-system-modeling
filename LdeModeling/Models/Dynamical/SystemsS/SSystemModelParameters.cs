namespace TestApp.Models.Dynamical.SystemsS
{
    public class SSystemModelParameters : ISSystemModelParameters
    {
        public CascadedPathawaySystemParameters ModelParameters { get; set; }

        object IDynamicalModelParameters.ModelParameters
        {
            get => ModelParameters;
            set
            {
                ModelParameters = (CascadedPathawaySystemParameters)value;
            }
        }

        public int StateDimension { get; }

        public double[] InitialState { get; set; }

        public int InputsNumber { get; }

        public int OutputsNumber => ModelParameters.OutputsNumber;

        public SSystemModelParameters(int stateDimension, int inputsNumber)
        {
            StateDimension = stateDimension;
            InputsNumber = inputsNumber;
        }
    }
}
