﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical.LinearDifferentialEquation.SingleOutput
{
    public class LdeSingleOutputModelParameters : ISingleOutputLdeModelParameters
    {
        public SingleOutputLdeParameters ModelParameters { get; set; }

        public int StateDimension { get; }

        public double[] InitialState { get; set; }

        public int InputsNumber { get; }

        public int OutputsNumber => ModelParameters.NumberOfOutputs;

        object IDynamicalModelParameters.ModelParameters
        {
            get => ModelParameters;
            set
            {
                ModelParameters = (SingleOutputLdeParameters)value;
            }
        }

        public LdeSingleOutputModelParameters(int stateDimension, int inputsNumber)
        {
            StateDimension = stateDimension;
            InputsNumber = inputsNumber;
        }
    }
}
