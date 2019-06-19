using OfficeOpenXml;
using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public class IterationValuesHistoryMaker : IIterationValueHistoryMaker
    {
        private List<double[]> _values;
        private readonly string _fileName = "Best alternative history";
        private readonly string _worksheetName = "Best alternative history";

        public AlgorithmStatsFollowerType Type => AlgorithmStatsFollowerType.ValuesHistory;

        public IterationValuesHistoryMaker()
        {
            _values = new List<double[]>();
        }

        public IIterationValuesHistoryStats GetStats()
        {
            return new IterationValuesHistoryStats(_values);
        }

        public void Refresh()
        {
            _values.Clear();
        }

        public void Update(IMessageToFollowers message)
        {
            _values.Add(message.AlternativeValues);
        }

        public void SaveToFile()
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(_worksheetName);

                // Generate colnames
                int maxNumberOfSolutions = _values.Max(v => v.Length);
                string[] objValuesColnames = new string[maxNumberOfSolutions];
                for (int i = 0; i < maxNumberOfSolutions; i++)
                {
                    objValuesColnames[i] = $"Solution {i + 1} obj";
                }

                // Write the column names
                for (int i = 0, j = 1; i < maxNumberOfSolutions; i++, j++)
                {
                    worksheet.Cells[1, j].Value = objValuesColnames[i];
                }

                // Fill in the values
                for (int i = 0, row = 2; i < _values.Count; i++, row++)
                {
                    for (int k = 0, col = 1; k < _values[i].Length; k++, col++)
                    {
                        worksheet.Cells[row, col].Value = _values[i][k];
                    }
                }

                excelPackage.SaveAs(new FileInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + _fileName));
            }
        }
    }
}
