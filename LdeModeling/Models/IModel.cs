using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models
{
    public interface IModel
    {
        IModelEvaluationParameters EvaluationParameters { get; }
        IModelParameters ModelParameters { get; }
        IModelOutput Evaluate(IModelInput input);
    }

    public interface IModel<TEvaluationParameters, TModelParameters, TModelOutput, TModelInput> : IModel
        where TEvaluationParameters : IModelEvaluationParameters
        where TModelParameters : IModelParameters
        where TModelOutput : IModelOutput
        where TModelInput : IModelInput
    {
        new TEvaluationParameters EvaluationParameters { get; }
        new TModelParameters ModelParameters { get; }
        TModelOutput Evaluate(TModelInput input);
    }
}
