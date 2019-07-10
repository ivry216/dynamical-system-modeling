namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public interface ILinearDifferentialEquationParameters : IDynamicalModelParameters
    {
        new LinearDynamicalSystemParameters ModelParameters { get; set; }
    }
}
