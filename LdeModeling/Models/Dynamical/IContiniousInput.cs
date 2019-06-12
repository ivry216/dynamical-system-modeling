using System;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public interface IContiniousInput : IDynamicalModelInput
    {
        new Func<double, double>[] Inputs { get; }
    }
}
