using System;
using System.Collections.Generic;
using System.Text;

namespace PredictItTradeHistoryCalculator
{
    public class Trade2
    {
        //DateExecuted,Type,MarketName,ContractName,Shares,Price,ProfitLoss,Fees,Risk,CreditDebit,URL
        public DateTime DateExecuted { get; set; }
        public string Type { get; set; }
        public string MarketName { get; set; }
        public string ContractName { get; set; }
        public int Shares { get; set; }
        public float Price { get; set; }
        public float ProfitLoss { get; set; }
        public float Fees { get; set; }
        public float Risk { get; set; }
        public float CreditDebit { get; set; }
        public string URL { get; set; }
    }
}
