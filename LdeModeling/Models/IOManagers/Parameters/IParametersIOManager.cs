using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.Models.IOManagers.Parameters
{
    public interface IParametersIOManager<TParameters>
        where TParameters : IModelParameters
    {
        void Save(TParameters modelParameters, string fileName);
    }
}
