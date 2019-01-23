﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical
{
    public interface IDiscreteInput : IDynamicalModelInput
    {
        new double[][] Inputs { get; }
    }
}
