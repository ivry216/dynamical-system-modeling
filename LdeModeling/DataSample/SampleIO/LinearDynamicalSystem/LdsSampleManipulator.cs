using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OfficeOpenXml;

namespace TestApp.DataSample.SampleIO.LinearDynamicalSystem
{
    public class LdsSampleManipulator : ISampleFileManipulator<DynamicalSystemSample>
    {
        public DynamicalSystemSample Load(string filePath)
        {
            throw new NotImplementedException();
        }

        public void Save(string filePath, DynamicalSystemSample sample)
        {
            // Save the data to file
            using (ExcelPackage package = new ExcelPackage(new FileInfo(filePath)))
            {
                // Get worksheet names 
                var names = package.Workbook.Worksheets.Select(ws => ws.Name);
                // Check if there is ws with required name
                if (names.Contains("Output"))
                    package.Workbook.Worksheets.Delete(package.Workbook.Worksheets.Where(ws => ws.Name == "Output").First());
                if (names.Contains("Input"))
                    package.Workbook.Worksheets.Delete(package.Workbook.Worksheets.Where(ws => ws.Name == "Input").First());

                // Create worksheet
                ExcelWorksheet wsData = package.Workbook.Worksheets.Add("Output");
                // Set column names
                int rowIndex = 1;
                wsData.Cells[rowIndex, 1].Value = "Time";
                for (int i = 2; i <= sample.Data.NumberOfOutputs + 1; i++)
                {
                    wsData.Cells[rowIndex, i].Value = i;
                }

                // Save the data
                // Save timepoints
                int colIndex = 1; // Index for column containing time
                rowIndex++;
                for (int i = 0, j = rowIndex; i < sample.Data.OutputSampleSize; i++, j++)
                {
                    wsData.Cells[j, colIndex].Value = sample.Data.OutputsTimes[i];
                }
                // Save output values
                colIndex = 2; // Index for column containing time
                for (int i = 0, row = rowIndex; i < sample.Data.OutputSampleSize; i++, row++)
                {
                    for (int output = 0, col = colIndex; output < sample.Data.NumberOfOutputs; output++, col++)
                    {
                        wsData.Cells[row, col].Value = sample.Data.Outputs[i][output];
                    }
                }

                // Create worksheet
                wsData = package.Workbook.Worksheets.Add("Input");
                // Set column names
                rowIndex = 1;
                wsData.Cells[rowIndex, 1].Value = "Time";
                for (int i = 2; i <= sample.Data.NumberOfInputs + 1; i++)
                {
                    wsData.Cells[rowIndex, i].Value = i;
                }

                // Save input values
                colIndex = 2; // Index for column containing time
                for (int i = 0, row = rowIndex; i < sample.Data.InputSampleSize; i++, row++)
                {
                    for (int input = 0, col = colIndex; input < sample.Data.NumberOfInputs; input++, col++)
                    {
                        wsData.Cells[row, col].Value = sample.Data.Inputs[i][input];
                    }
                }

                package.Save();
            }
        }
    }
}
