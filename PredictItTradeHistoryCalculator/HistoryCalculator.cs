using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PredictItTradeHistoryCalculator
{
    public class HistoryCalculator
    {
        private readonly string _filePath = @"C:\Users\Josh\Documents\PredictIt\TradeHistory.csv";
        //private readonly string _filePath = @"C:\Users\Josh\Documents\PredictIt\TradeHistory Jasperson.csv";
        public void Run()
        {
            var trades = ReadCsv();

            //PrintAllTrades(trades);

            //PrintMonthlyStats(trades);

            //PrintBreakdownByPrice(trades);

            //PrintMarketBreakdown(trades, "Will NASA find");
            //PrintMarketBreakdown(trades, "What will be the Electoral College margin in the 2020 presidential election");
            //PrintMarketBreakdown(trades, "Who will win the 2020 U.S. presidential election?");
            //PrintMarketBreakdown(trades, "Which party will win the 2020 U.S. presidential election?");
            //PrintMarketBreakdown(trades, "Who will win the 2020 Democratic vice presidential nomination?");
            //PrintMarketBreakdown(trades, "Who will win the 2020 Democratic presidential nomination?");
            //PrintMarketBreakdown(trades, "Will Hillary Clinton  run for president in 2020?");
            //PrintMarketBreakdown(trades, "Will the US economy hit 5.0%+ GDP growth by year-end 2020?");
            //PrintMarketBreakdown(trades, "Who will win the 2020 Republican presidential nomination?");
            //PrintMarketBreakdown(trades, "Who will win the 2020 Iowa Democratic caucuses?");
            //PrintMarketBreakdown(trades, "Who will win the 2020 New Hampshire Democratic primary?");

            var markets = GetMarkets(trades);
            foreach (var market in markets.OrderBy(s => s.Profit))
            {
                if (Math.Abs(market.Profit) > 5)
                    market.Print();
            }

            //PrintContractInMarket("Who will win the 2020 U.S. presidential election", "Trump", 14, trades);
            //PrintContractInMarket("Who will win the 2020 U.S. presidential election", "Biden", 14, trades);
            //PrintContractInMarket("Which party will win the 2020 U.S. presidential election", "Democratic", 14, trades);
            //PrintContractInMarket("Which party will win the 2020 U.S. presidential election", "Republican", 14, trades);

            //PrintContractInMarket("What will be the Electoral College margin in the 2020 presidential election", "Dems-by", 14, trades);
            //PrintContractInMarket("What will be the Electoral College margin in the 2020 presidential election", "GOP-by", 14, trades);

            //PrintContractInMarket("What will be the popular vote margin in the 2020 presidential election", "Dems-by", 14, trades);
            //PrintContractInMarket("What will be the popular vote margin in the 2020 presidential election", "GOP-by", 14, trades);

            //PrintContractInMarket("Will Alexandria Ocasio-Cortez win the Democratic primary for NY's 14th District", "Yes", 60, trades);
            //PrintContractInMarket("Will Hillary Clinton  run for president in 2020", "No", 100, trades);
            //PrintContractInMarket("Will Michelle Obama run for president in 2020", "No", 100, trades);
            //PrintContractInMarket("Will Rashida Tlaib win the Democratic primary for Michigan's 13th District", "Yes", 100, trades);
        }

        private void PrintContractInMarket(string market, string contract, int numDays, List<Trade> trades)
        {
            var marketTrades = trades.Where(t => t.MarketName.Contains(market)).ToList();
            var days = DateTime.Now.AddDays(numDays * -1);
            var lastMonth = marketTrades.Where(t => t.Date > days).ToList();
            var contractTrades = lastMonth.Where(t => t.Contract.Contains(contract)).ToList();

            var totalProfit = contractTrades.Sum(t => t.ProfitLessFees);
            var averageCost = contractTrades.Average(t => t.SharePrice);
            var profitPerShare = contractTrades.Where(t => t.Type != TransactionType.Buy).Average(t => t.ProfitPerShare).ToString("#,##0.000").PadLeft(7);
            var totalSharesTraded = contractTrades.Sum(t => t.Shares);

            Console.WriteLine($"Market: {market}");
            Console.WriteLine($"Contract:{contract}   Days Back:{numDays}   Profit:{totalProfit:c}   Average Cost:{averageCost:c}  Profit/Share{profitPerShare}  Shares Traded:{totalSharesTraded}\n");
            //Console.WriteLine($"US Presidency Trump trades- ");
        }

        public List<Market> GetMarkets(List<Trade> trades)
            => trades.GroupBy(g => g.MarketName).Select(GetMarket).ToList();

        private Market GetMarket(IGrouping<string, Trade> markets)
        {
            var marketTrades = markets.ToList();
            var market =  new Market
            {
                Name = markets.Key,
                Trades = marketTrades.Count,
                Profit = marketTrades.Sum(s => s.ProfitLessFees),
                //Average = marketTrades.Where(t => t.Type != TransactionType.Buy).Average(s => s.ProfitLessFees),
                Shares = marketTrades.Sum(s => s.Shares),
            };

            var transactionAverages = marketTrades.Where(t => t.Type != TransactionType.Buy);
            if (transactionAverages.Any())
                market.Average = transactionAverages.Average(s => s.ProfitLessFees);

            return market;
        }

        private void PrintMarketBreakdown(List<Trade> trades, string marketName)
        {
            var marketTrades = trades.Where(t => t.MarketName.Contains(marketName)).ToList();

            var total = marketTrades.Count;
            var profit = marketTrades.Sum(s => s.ProfitLessFees);
            float average = 0;
            if (marketTrades.Any())
                average = marketTrades.Where(t => t.Type != TransactionType.Buy).Average(s => s.ProfitLessFees);

            Console.WriteLine($"Market:{ marketTrades.FirstOrDefault()?.MarketName}");
            Console.WriteLine($"Trades:{total} Average:{average:c} Total Profit:{profit:c}\n");
        }

        private void PrintBreakdownByPrice(List<Trade> trades)
        {
            Console.WriteLine("\n-----Price Breakdown-----");
            for (int i = 0; i < 10; i++)
            {
                var num = i * 10;
                var lowerLimit = num;
                var upperLimit = num + 10;
                var tranche = trades.Where(s => lowerLimit <= s.SharePrice * 100 && s.SharePrice * 100 <= upperLimit && s.Type != TransactionType.Buy).ToList();
                var total = tranche.Count;
                var profit = tranche.Sum(s => s.Profit).ToString("$###0").PadLeft(7);
                var profitLessFees = tranche.Sum(s => s.ProfitLessFees).ToString("$###0").PadLeft(7);
                var fees = tranche.Sum(s => s.Fee).ToString("$#,##0.00").PadLeft(7);
                var tradedShares = $"{tranche.Sum(t => t.Shares)}".PadLeft(6);
                var profitPerShare = tranche.Average(g => g.ProfitPerShare).ToString("$#,##0.000").PadLeft(7);
                string average = string.Empty;
                if (tranche.Any())
                {
                    average = tranche.Average(s => s.Profit).ToString("$#,##0.00").PadLeft(6);
                }

                var lowerBound = $"$0.{num}".PadLeft(5);
                var upperBound = $"$0.{num+10}".PadLeft(6);
                Console.WriteLine($"{lowerBound}-{upperBound}:  Trades:{total:D3}  Shares:{tradedShares}  Total Profit:{profit}   Fees:{fees}   Profit-Fees{profitLessFees:c}   PennyProfit/Share:{profitPerShare}");
            }
        }

        private void PrintMonthlyStats(List<Trade> trades)
        {
            Console.WriteLine("\n-----Monthly Breakdown-----");
            foreach (var trade in trades.GroupBy(t => t.Date.Month))
            {
                var month = trade.First().Date.ToString("MMM");
                var average = trade.ToList().Where(t => t.Type != TransactionType.Buy).Average(g => g.ProfitLessFees).ToString("$#,##0.00").PadLeft(6);
                var profitPerShare = trade.ToList().Where(t => t.Type != TransactionType.Buy).Average(g => g.ProfitPerShare).ToString("$#,##0.000").PadLeft(7);
                var totalShares = $"{trade.ToList().Sum(t => t.Shares)}".PadLeft(6);
                var total = trade.ToList().Sum(s => s.ProfitLessFees).ToString("$###0").PadLeft(6);
                var numTrades = $"{trade.Count()}".PadLeft(4);
                Console.WriteLine($"{month}   Avg:{average}   Trades:{numTrades}  Shares:{totalShares}  Profit:{total}   PennyProfit/Share:{profitPerShare}");
            }
        }

        private void PrintAllTrades(List<Trade> trades)
        {
            float total = 0;
            int count = 0;
            foreach (var trade in trades.OrderBy(t => t.Profit))
            {
                if (trade.Profit != 0)
                {
                    Console.WriteLine($"{count}-{trade.Profit:c} - {trade.Date:yyyy-MM-dd} - {trade.MarketName} - {trade.Contract}");
                    count++;
                }

                total += trade.Profit;
            }
            Console.WriteLine($"total: ${total}");
        }

        private List<Trade> ReadCsv()
        {
            var trades = new List<Trade>();
            using(var file = new StreamReader(_filePath))
            {
                string line;

                while((line = file.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.Contains("DateExecuted,Type"))
                        trades.Add(new Trade(line));
                }
            }
            return trades;
        }
    }
}
