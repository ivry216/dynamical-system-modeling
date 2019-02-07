using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.IOManagers
{
    public interface IModelingResultsIOManager<TModelOutput, TModelInput>
        where TModelOutput : IModelOutput
        where TModelInput : IModelInput
    {
        void Save(TModelOutput modelOutput, string fileName);
        void Save(TModelOutput modelOutput, TModelInput modelInput, string fileName);
    }
}
