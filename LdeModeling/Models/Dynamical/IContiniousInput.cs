﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation
{
    public interface IContiniousInput : IDynamicalModelInput
    {
        new Func<double, double>[] Inputs { get; }
    }
}
