using System;

namespace PredictItTradeHistoryCalculator
{
    public class Market
    {
        public int Trades { get; set; }
        public string Name { get; set; }
        public float Profit { get; set; }
        public DateTime AverageDate { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LasteDate { get; set; }
        public float Average { get; set; }
        public int Shares { get; set; }
        public float ProfitPerShare => (Profit / Shares ) * 100;

        public void Print()
        {
            Console.WriteLine($"Market:{Name}");
            var pennyProfit = ProfitPerShare.ToString("#,##0.00");
            var totalProfit = Profit.ToString("$#,##0.00");
            Console.WriteLine($"Trades:{Trades}   Shares:{Shares}   PennyProfit/Share:{pennyProfit}   Total Profit:{totalProfit}\n");
        }
    }
}
