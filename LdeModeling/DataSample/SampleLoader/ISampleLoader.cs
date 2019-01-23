using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataSample.SampleLoader
{
    interface ISampleLoader<TSample>
        where TSample : ISample
    {
        string FilePath { get; set; }
        TSample Data { get; }

        void Load();
    }
}
