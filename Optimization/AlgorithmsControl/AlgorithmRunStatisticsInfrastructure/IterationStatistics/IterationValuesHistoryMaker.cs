using OfficeOpenXml;
using Optimization.AlgorithmsControl.AlgorithmRunStatistics;
using System.Collections.Generic;

namespace Optimization.AlgorithmsControl.AlgorithmRunStatisticsInfrastructure.IterationStatistics
{
    public class IterationValuesHistoryMaker : IIterationValueHistoryMaker
    {
        private List<double[]> values;

        public AlgorithmStatsFollowerType Type => AlgorithmStatsFollowerType.ValuesHistory;

        public IterationValuesHistoryMaker()
        {
            values = new List<double[]>();
        }

        public IIterationValuesHistoryStats GetStats()
        {
            return new IterationValuesHistoryStats(values);
        }

        public void Refresh()
        {
            values.Clear();
        }

        public void Update(IMessageToFollowers message)
        {
            values.Add(message.AlternativeValues);
        }

        public void SaveToFile()
        {
            using (ExcelPackage excel)
            {

            }
        }
    }
}
