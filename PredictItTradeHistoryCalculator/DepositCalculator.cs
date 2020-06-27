using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PredictItTradeHistoryCalculator
{
    public class DepositCalculator
    {
        //private readonly string _historyCsvPath = @"C:\Users\Josh\Documents\PredictIt\WalletHistory.csv";
        private readonly string _historyCsvPath = @"C:\Users\Josh\Documents\PredictIt\WalletHistory Jasperson.csv";
        public void Run()
        {
            var transactions = ReadCsv();

            //foreach(var transaction in transactions)
            //{
            //    Console.WriteLine($"Deposit:{transaction.IsDeposit} Date:{transaction.Date.ToShortDateString()} Amount:{transaction.Amount:c} Fee:{transaction.Fees:c}");
            //}

            DisplayTotals(transactions);
        }

        private void DisplayTotals(List<DepositWithdrawal> transactions)
        {
            var deposits = transactions.Where(t => t.IsDeposit).ToList();
            var totalDeposits = deposits.Sum(s => s.Amount);

            Console.WriteLine($"Total Deposits:{deposits.Count} Amount:{totalDeposits:C}");

            var withdrawals = transactions.Where(t => !t.IsDeposit).ToList();
            var totalWithdrawn = withdrawals.Sum(w => w.Amount);
            var totalFees = withdrawals.Sum(w => w.Fees);
            Console.WriteLine($"---Withdrawals:{withdrawals.Count}  Amount:{totalWithdrawn:c} Fees:{totalFees:c}");

            var expectedTotal = totalDeposits - (totalWithdrawn + totalFees);
            Console.WriteLine($"Expected Account Value:{expectedTotal:c}");
        }

        private List<DepositWithdrawal> ReadCsv()
        {
            var transactions = new List<DepositWithdrawal>();
            using(var file = new StreamReader(_historyCsvPath))
            {
                string line;
                while ((line = file.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.Contains("Type,Date and Time,Confirmation Number,"))
                        transactions.Add(new DepositWithdrawal(line));
                }
            }
            return transactions;
        }
    }

    public class DepositWithdrawal
    {
        //Type,Date and Time,Confirmation Number,Net Amount,Fee,Status
        public DepositWithdrawal(string line)
        {
            var props = line.Split(",");
            IsDeposit = props[0].Contains("Deposit");
            Date = DateTime.Parse(props[1].Replace("ET", ""));
            Amount = float.Parse(props[3].Replace("$", "").Replace("\"",""));

            var fee = props[4];
            if (!string.IsNullOrEmpty(fee))
            {
                fee = fee.Replace("(", "");
                fee = fee.Replace(")", "");
                Fees = float.Parse(fee.Replace("$", "").Replace("\"", ""));
            }
        }

        public bool IsDeposit { get; set; }
        public DateTime Date { get; set; }
        public float Amount { get; set; }
        public float Fees { get; set; }
    }
}