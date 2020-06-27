using CsvHelper;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Text;

namespace PredictItTradeHistoryCalculator
{
    public class ProfitLossCalculator
    {
        private readonly string _filePath = @"C:\Users\Josh\Documents\PredictIt\TradeHistory.csv";
        public void Run()
        {
            var trades = ReadCsv();

        }

        private List<Trade2> ReadCsv()
        {
            using(var reader = new StreamReader(_filePath))
            using (var csv = new CsvReader(reader, CultureInfo.InvariantCulture))
            {
                var records = csv.GetRecords<Trade2>();
                return records.ToList();
            }
            return null;
        }
    }
}
