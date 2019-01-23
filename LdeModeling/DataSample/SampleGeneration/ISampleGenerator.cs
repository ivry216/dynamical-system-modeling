using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.DataSample;

namespace TestApp.DataSample.SampleGeneration
{
    public interface ISampleGenerator<TSample>
        where TSample : ISample
    {
        int SampleSize { get; set; }

        TSample Generate();
    }
}
