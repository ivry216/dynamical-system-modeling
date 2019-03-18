using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataSample.SampleIO
{
    interface ISampleFileManipulator<TSample>
        where TSample : ISample
    {
        TSample Load(string filePath);
        void Save(string filePath, TSample sample);
    }
}
