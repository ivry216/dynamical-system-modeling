using OfficeOpenXml;
using System;
using System.IO;
using System.Linq;
using TestApp.Optimization.AlgorithmsControl.AlgorithmMeta;
using TestApp.Optimization.AlgorithmsControl.AlgorithmRunStatistics;

namespace TestApp.Optimization.AlgorithmsControl.IOManagers
{
    public class StandardLauncherStatisticsIOManager : ITestSessionResultsIOManager<IContainingStats<IBestVariableAndValueStats>, IBestVariableAndValueStats>
    {
        // TODO: improve the method
        public void SaveStats(IContainingStats<IBestVariableAndValueStats> launcher, string fileName)
        {
            // Assign the stats to the new variable
            var stats = launcher.Stats;

            //
            // Save the data to file
            using (ExcelPackage package = new ExcelPackage(new FileInfo(fileName)))
            {
                // Get worksheet names 
                var names = package.Workbook.Worksheets.Select(ws => ws.Name);
                // Check if there is ws with required name
                if (names.Contains("LaunchStatistics"))
                    package.Workbook.Worksheets.Delete(package.Workbook.Worksheets.Where(ws => ws.Name == "LaunchStatistics").First());

                // Create worksheet
                ExcelWorksheet wsData = package.Workbook.Worksheets.Add("LaunchStatistics");
                // Set column names
                int rowIndex = 1;
                wsData.Cells[rowIndex, 1].Value = "Launch";
                wsData.Cells[rowIndex, 2].Value = "BestValue";

                // Problem dimension
                var dimension = stats.First().BestSolution.Length;

                for (int i = 3, j = 1; j <= dimension; i++, j++)
                {
                    wsData.Cells[rowIndex, i].Value = $"Variable {j}";
                }

                // Save the data
                // Save launch indices
                int colIndex = 1; // Index for column containing time
                rowIndex++;
                for (int i = 0, j = rowIndex; i < stats.Count; i++, j++)
                {
                    wsData.Cells[j, colIndex].Value = i + 1;
                }

                // Save launch statistics
                colIndex = 2;
                for (int i = 0, row = rowIndex; i < stats.Count; i++, row++)
                {
                    // Save value
                    wsData.Cells[row, 2].Value = stats[i].BestValue;
                    // Save best solution found
                    for (int varialbeIndex = 0, col = colIndex + 1; varialbeIndex < dimension; varialbeIndex++, col++)
                    {
                        wsData.Cells[row, col].Value = stats[i].BestSolution[varialbeIndex];
                    }
                }

                package.Save();
            }
        }
    }
}
