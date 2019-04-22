using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TestApp.Models.Dynamical.LinearDifferentialEquation;

namespace TestApp.Models.IOManagers.Parameters.Dynamical
{
    public class DynamicalModelParametersIOManager : IParametersIOManager<LdeModelParameters>
    {
        public void Save(LdeModelParameters modelParameters, string fileName)
        {
            // Save the data to file
            using (ExcelPackage package = new ExcelPackage(new FileInfo(fileName)))
            {
                // Get worksheet names 
                var names = package.Workbook.Worksheets.Select(ws => ws.Name);
                // Check if there is ws with required name
                if (names.Contains("Parameters"))
                    package.Workbook.Worksheets.Delete(package.Workbook.Worksheets.Where(ws => ws.Name == "Parameters").First());

                // Create worksheet
                ExcelWorksheet wsPars = package.Workbook.Worksheets.Add("Parameters");

                // Initialize column indices
                int indexMatrix = 1;
                int indexRows = 2;
                int indexColumns = 3;
                int indexValues = 4;

                // Add colnames
                int currentRow = 1;
                wsPars.Cells[currentRow, indexMatrix].Value = "Matrix";
                wsPars.Cells[currentRow, indexRows].Value = "Row";
                wsPars.Cells[currentRow, indexColumns].Value = "Column";
                wsPars.Cells[currentRow, indexValues].Value = "Value";

                // Fill in values
                currentRow = 2;
                // Fill in the system matrix
                for (int i = 0; i < modelParameters.StateDimension; i++)
                {
                    for (int j = 0; j < modelParameters.StateDimension; j++)
                    {
                        wsPars.Cells[currentRow, indexMatrix].Value = "A";
                        wsPars.Cells[currentRow, indexRows].Value = i;
                        wsPars.Cells[currentRow, indexColumns].Value = j;
                        wsPars.Cells[currentRow, indexValues].Value = modelParameters.ModelParameters.A[i,j];
                        currentRow++;
                    }
                }
                // Fill in the control parameters
                for (int i = 0; i < modelParameters.StateDimension; i++)
                {
                    for (int j = 0; j < modelParameters.InputsNumber; j++)
                    {
                        wsPars.Cells[currentRow, indexMatrix].Value = "B";
                        wsPars.Cells[currentRow, indexRows].Value = i;
                        wsPars.Cells[currentRow, indexColumns].Value = j;
                        wsPars.Cells[currentRow, indexValues].Value = modelParameters.ModelParameters.B[i, j];
                        currentRow++;
                    }
                }

                package.Save();
            }
        }
    }
}
