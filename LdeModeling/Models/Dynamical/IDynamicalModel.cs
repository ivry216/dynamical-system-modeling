using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.Dynamical
{
    public interface IDynamicalModel<TEvaluationParameters, TModelParameters, TOutput, TInput> : IModel<TEvaluationParameters, TModelParameters, TOutput, TInput>
        where TEvaluationParameters : IDynamicalModelEvaluationParams
        where TModelParameters : IDynamicalModelParameters
        where TOutput : IDynamicalModelOutput
        where TInput : IDynamicalModelInput
    {

    }
}
