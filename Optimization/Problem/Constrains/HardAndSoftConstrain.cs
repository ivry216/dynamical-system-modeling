﻿using System;

namespace Optimization.Problem.Constrains
{
    public class HardAndSoftConstrain
    {
        public double? SoftMaxConstrain { get; }
        public double? SoftMinConstrain { get; }
        public double? HardMaxConstrain { get; }
        public double? HardMinConstrain { get; }

        public double SoftConstrainCoeff { get; }

        public HardAndSoftConstrain(double? hardMin, double? hardMax, double? softMin, double? softMax, double sotfConstrainCoeff = 0d)
        {
            HardMaxConstrain = hardMax;
            HardMinConstrain = hardMin;
            SoftMinConstrain = softMin;
            SoftMaxConstrain = softMax;
            SoftConstrainCoeff = sotfConstrainCoeff;
        }

        public bool IsFeasible(double alternativeValue)
        {
            if (HardMaxConstrain.HasValue && HardMaxConstrain.Value < alternativeValue)
            {
                return false;
            }

            if (HardMinConstrain.HasValue && HardMinConstrain.Value > alternativeValue)
            {
                return false;
            }

            return true;
        }

        public double CalculatePenalty(double alternativeValue)
        {
            if (SoftMaxConstrain.HasValue && SoftMaxConstrain.Value < alternativeValue)
            {
                return Math.Abs(SoftMaxConstrain.Value - alternativeValue);
            }

            if (SoftMinConstrain.HasValue && SoftMinConstrain.Value > alternativeValue)
            {
                return Math.Abs(SoftMinConstrain.Value - alternativeValue);
            }

            return 0;
        }
    }
}
