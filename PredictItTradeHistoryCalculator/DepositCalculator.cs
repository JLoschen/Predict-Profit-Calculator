using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace PredictItTradeHistoryCalculator
{
    public class DepositCalculator
    {
        private readonly string _historyCsvPath = @"C:\Users\Josh\Documents\PredictIt\WalletHistory.csv";
        //private readonly string _historyCsvPath = @"C:\Users\Josh\Documents\PredictIt\WalletHistory Jasperson.csv";
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
                float total = 0;
                string line;
                var lines = new List<string>();
                while ((line = file.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.Contains("Type,Date and Time,Confirmation Number,"))
                    {
                        var deposit = new DepositWithdrawal(line);
                        transactions.Add(deposit);
                    }
                }

                transactions.Reverse();
                foreach(var transaction in transactions)
                {
                    if (transaction.IsDeposit)
                    {
                        total += transaction.Amount;
                    }
                    else
                    {
                        total -= transaction.Amount;
                    }
                    //if (IsDeposit)
                    //{
                    //    
                    //    Console.WriteLine($"adding {amount} {Date.ToShortDateString()}");
                    //}
                    var amount = $"{transaction.Amount:c0}".PadLeft(7);
                    //var amount = transaction.Amount.ToString("$###0").PadLeft(6);
                    var action = transaction.IsDeposit ? "    adding" : "withdrawal";
                    var totalDisplay = $"{total:c0}".PadLeft(7);
                    //var totalDisplay = total.ToString("$###0").PadLeft(7);
                /*.ToShortDateString()*/
                Console.WriteLine($"{transaction.Date:yyyy'-'MM'-'dd} {action} {amount} -> {totalDisplay}");
                }
                //lines.Reverse();
                //foreach (var l in lines)
                //{
                //    Console.WriteLine(l);
                //}
            }
            return transactions;
        }
    }

    public class DepositWithdrawal
    {
        //Type,Date and Time,Confirmation Number,Net Amount,Fee,Status
        public DepositWithdrawal(string line)
        {

            line = line.Replace("$1,", "1");
            line = line.Replace("$2,", "2");
            line = line.Replace("$3,", "3");
            line = line.Replace("$4,", "4");
            line = line.Replace("$5,", "5");
            line = line.Replace("$6,", "6");
            line = line.Replace("$7,", "7");
            line = line.Replace("$8,", "8");
            line = line.Replace("$9,", "9");

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