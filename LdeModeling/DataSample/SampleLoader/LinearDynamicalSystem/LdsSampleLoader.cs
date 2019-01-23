using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace TestApp.DataSample.SampleLoader.LinearDynamicalSystem
{
    public class LdsSampleLoader : ISampleLoader<DynamicalSystemSample>
    {
        private DynamicalSystemSample _data;

        public string FilePath { get; set; }

        public DynamicalSystemSample Data => _data ?? throw new Exception("Data is not loaded");

        public void Load()
        {
            // Load package
            var package = new ExcelPackage(new FileInfo(FilePath));
            // Get worksheet
            ExcelWorksheet worksheet = package.Workbook.Worksheets[1];
        }
    }
}
