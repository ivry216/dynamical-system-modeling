using MathCore.Extensions.Arrays;
using OfficeOpenXml;
using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using System.Collections.Generic;
using System.IO;
using System.Reflection;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public class BestAlternativeHistoryMaker : IBestAlternativeHistoryMaker
    {
        private List<double[]> _bestAlternatives;
        private List<double> _bestValues;

        private readonly string _fileName = "History best alternative.xlsx";
        private readonly string _worksheetName = "Best alternative history";

        public AlgorithmStatsFollowerType Type => AlgorithmStatsFollowerType.BestAlternativeHistory;

        public BestAlternativeHistoryMaker()
        {
            _bestAlternatives = new List<double[]>();
            _bestValues = new List<double>();
        }

        public IBestAlternativeHistoryStats GetStats()
        {
            return new BestAlternativeHistoryStats(_bestValues, _bestAlternatives);
        }

        public void Refresh()
        {
            _bestAlternatives.Clear();
            _bestValues.Clear();
        }

        public void Update(IMessageToFollowers message)
        {
            _bestAlternatives.Add(message.BestAlternative.CopyVector());
            _bestValues.Add(message.BestValue);
        }

        public void SaveToFile()
        {
            using (ExcelPackage excelPackage = new ExcelPackage())
            {
                ExcelWorksheet worksheet = excelPackage.Workbook.Worksheets.Add(_worksheetName);

                // Generate alternatives colname
                int dimension = _bestAlternatives[0].Length;
                string[] alternativesColnames = new string[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    alternativesColnames[i] = $"Alternative coord {i+1}";
                }

                // Value colname
                string valueColname = "Objective value";
                int valueColnameIndex = dimension + 1;

                // Write the column names
                for (int i = 0, j = 1; i < dimension; i++, j++)
                {
                    worksheet.Cells[1, j].Value = alternativesColnames[i];
                }
                worksheet.Cells[1, dimension + 2].Value = valueColname;

                // Fill in the values
                for (int i = 0, row = 2; i < _bestAlternatives.Count; i++, row++)
                {
                    for (int k = 0, col = 1; k < dimension; k++, col++)
                    {
                        worksheet.Cells[row, col].Value = _bestAlternatives[i][k];
                    }
                    worksheet.Cells[row, valueColnameIndex].Value = _bestValues[i];
                }

                excelPackage.SaveAs(new FileInfo(Path.GetDirectoryName(Assembly.GetEntryAssembly().Location) + "\\" + _fileName));
            }
        }
    }
}
