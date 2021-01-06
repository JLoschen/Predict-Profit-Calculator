using System;
using System.Linq;

namespace PredictItTradeHistoryCalculator
{
    public class Trade
    {
        //DateExecuted,Type,MarketName,ContractName,Shares,Price,ProfitLoss,Fees,Risk,CreditDebit,URL
        public Trade(string line)
        {
            //int count = source.Count(f => f == '/');
            var totalQuotes = line.Count(c => c == '"');
            if(totalQuotes % 2 != 0)
            {
                Console.WriteLine("problem!");
            }

            line = line.Replace("\\\"", string.Empty);

            if(totalQuotes > 2)
            {
                Console.WriteLine("too many quotes");
            }

            if(totalQuotes == 2)
            {
                var index1 = line.IndexOf('"');
                var index2 = line.LastIndexOf('"');
                string commaText = line.Substring(index1, index2 - index1);
                string withoutCommaText = commaText.Replace(",", "");
                line = line.Replace(commaText, withoutCommaText);
                //Console.WriteLine(commaText);
            }

            var props = line.Split(",");
            Date = DateTime.Parse(props[0]);
            MarketName = props[2];
            Contract = props[3];
            Price = float.Parse(props[5].Replace("$", ""));
            //IsBuy = props[1].Contains("Buy");
            Fee = float.Parse(props[7].Replace("$", "").Replace("(", "").Replace(")", ""));
            Shares = int.Parse(props[4]);

            var profit = props[6].Replace("$","");
            if (profit.StartsWith("("))
            {
                profit = profit.Replace("(", "");
                profit = profit.Replace(")", "");
                Profit = float.Parse(profit) * -1;
            }
            else
            {
                Profit = float.Parse(profit);
            }

            ProfitLessFees = Profit - Fee;

            if (props[1].Contains("Close"))
            {
                Type = TransactionType.Close;   
            }
            else if (props[1].Contains("Sell"))
            {
                Type = TransactionType.Sell;
            }
            else
                Type = TransactionType.Buy;

            var profitPerShare = (Profit / Shares);
            ProfitPerShare = profitPerShare * 100;

            SharePrice = Price - profitPerShare;
        }

        public DateTime Date { get; set; }
        public bool IsBuy { get; set; }
        public bool IsYes { get; set; }
        public float Profit { get; set; }
        public float ProfitLessFees { get; set; }
        public float Fee { get; set; }
        public float Cash { get; set; }
        public float Price { get; set; }
        public int Shares { get; set; }
        public string MarketName { get; set; }
        public string Contract { get; set; }
        public TransactionType Type { get; set; }
        public float ProfitPerShare { get; set; }
        public float SharePrice { get; set; }
    }

    public enum TransactionType
    {
        Buy,
        Sell,
        Close
    }
}
