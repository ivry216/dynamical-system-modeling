﻿using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.Dynamical;

namespace TestApp.Models.ModelingResults.Dynamical
{
    public class DynamicalModelResultsIOManager
        : IModelingResultsIOManager<DiscreteDynamicalModelOutput, DiscreteDynamicalModelInput>
    {
        public void Save(DiscreteDynamicalModelOutput modelOutput, string fileName)
        {
            // Save the data to file
            using (ExcelPackage package = new ExcelPackage(new FileInfo(fileName)))
            {
                // Create worksheet
                ExcelWorksheet wsData = package.Workbook.Worksheets.Add("Data");
                // Set column names
                int rowIndex = 1;
                wsData.Cells[rowIndex, 1].Value = "Time";
                for (int i = 2; i <= modelOutput.NumberOfOutputs + 1; i++)
                {
                    wsData.Cells[rowIndex, i].Value = i;
                }

                // Save the data
                // Save timepoints
                int colIndex = 1; // Index for column containing time
                rowIndex++;
                for (int i = 0, j = rowIndex; i < modelOutput.Count; i++, j++)
                {
                    wsData.Cells[j, colIndex].Value = modelOutput.Times[i];
                }
                // Save output values
                colIndex = 2; // Index for column containing time
                for (int i = 0, row = rowIndex; i < modelOutput.Count; i++, row++)
                {
                    for (int output = 0, col = colIndex; output < modelOutput.NumberOfOutputs; output++, col++)
                    {
                        wsData.Cells[row, col].Value = modelOutput.Outputs[i][output];
                    }
                }

                package.Save();
            }
        }

        public void Save(DiscreteDynamicalModelOutput modelOutput, DiscreteDynamicalModelInput modelInput, string fileName)
        {
            throw new NotImplementedException();
        }
    }
}
