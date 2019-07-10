namespace TestApp.Models.Dynamical.LinearDifferentialEquation.SingleOutput
{
    public interface ISingleOutputLdeModelParameters : IDynamicalModelParameters
    {
        new SingleOutputLdeParameters ModelParameters { get; set; }
    }
}
