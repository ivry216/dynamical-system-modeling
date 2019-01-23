using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataSample
{
    public interface IData
    {
        DataType DataType { get; }
        int OutputSampleSize { get; }
    }
}
