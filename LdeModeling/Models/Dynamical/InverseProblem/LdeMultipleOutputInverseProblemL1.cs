﻿using MathCore.Extensions.Arrays;

namespace TestApp.Models.Dynamical.InverseProblem
{
    public class LdeMultipleOutputInverseProblemL1 : LdeMultipleOutputInverseProblem
    {
        #region Properties

        public double Lambda { get; set; }

        #endregion Properties

        #region Constructor

        public LdeMultipleOutputInverseProblemL1(int dimension) : base(dimension)
        {
        }

        #endregion Construtor

        #region Setting

        public override double CalcualteCriterion(double[] alternative)
        {
            model.ModelParameters.ModelParameters.AssignWithArray(alternative);

            double result = modelToDataProcessor.CalculateMultipleOutputCriterion(model);

            return 1 / (1 + result + Lambda * alternative.CalculateL1Norm());
        }

        #endregion Setting
    }
}