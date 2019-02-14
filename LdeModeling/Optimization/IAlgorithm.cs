﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Optimization
{
    public interface IAlgorithm
    {
        object BestValue { get; }
        object BestSolution { get; }
        void SetParameters(object parameters);
        void Evaluate();
    }
}