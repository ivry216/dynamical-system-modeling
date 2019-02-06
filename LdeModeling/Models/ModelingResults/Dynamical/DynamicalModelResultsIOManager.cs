using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.Dynamical;

namespace TestApp.Models.ModelingResults.Dynamical
{
    public class DynamicalModelResultsIOManager
        : IModelingResultsIOManager<DiscreteDynamicalModelOutput, DiscreteDynamicalModelInput>
    {
        public void Save(DiscreteDynamicalModelOutput modelOutput, string fileName)
        {
            throw new NotImplementedException();
        }

        public void Save(DiscreteDynamicalModelOutput modelOutput, DiscreteDynamicalModelInput modelInput, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
