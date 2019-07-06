namespace TestApp.Models.Dynamical.SystemsS
{
    public interface ISSystemModelParameters : IDynamicalModelParameters
    {
        new CascadedPathawaySystemParameters ModelParameters { get; set; }
    }
}
