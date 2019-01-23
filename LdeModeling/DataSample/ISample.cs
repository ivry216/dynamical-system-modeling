using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TestApp.DataSample
{
    public interface ISample
    {
        int Size { get; }
        object Data { get; }
    }

    public interface ISample<TData> : ISample
        where TData : IData
    {
        new TData Data { get; }
    }
}
