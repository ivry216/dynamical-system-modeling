using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataSample
{
    public class DynamicalSystemSample : ISample<IDynamicalRegressionData>
    {
        private readonly IDynamicalRegressionData _dynamicalSystemData;

        public int Size => _dynamicalSystemData.OutputSampleSize;

        public IDynamicalRegressionData Data => _dynamicalSystemData;

        object ISample.Data => Data;

        public DynamicalSystemSample(IDynamicalRegressionData labeledRegressionData)
        {
            _dynamicalSystemData = new DynamicalRegressionData(labeledRegressionData.OutputsTimes, 
                labeledRegressionData.Outputs, labeledRegressionData.InputsTimes, labeledRegressionData.Inputs);
        }
    }
}
