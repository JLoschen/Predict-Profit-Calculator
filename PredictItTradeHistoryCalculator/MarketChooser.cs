using System;
using System.Collections.Generic;
using System.Linq;

namespace PredictItTradeHistoryCalculator
{
    public class MarketChooser
    {
        public void Run()
        {
            PrintOrderedByROI(GetMarketsAfterElection());
        }

        private List<MarketChoice> GetMarketsAfterElection() => new List<MarketChoice>()
            {
                new MarketChoice("Will Donald Trump complete his first term", new DateTime(2021, 1, 20), 0.85m, 0.95m),
                new MarketChoice("Will the Senate convict Donald Trump on impeachment in his first term?", new DateTime(2021, 1, 20), 0.97m, 0.99m),
                new MarketChoice("Will Trump resign during his first term?", new DateTime(2021, 1, 20), 0.87m, 0.96m),
                new MarketChoice("Will a federal charge against Hunter Biden be confirmed by Dec. 31, 2020?", new DateTime(2020, 12, 31), 0.80m, 0.95m),
                new MarketChoice("Will a federal charge against Rudy Giuliani be confirmed by Dec. 31, 2020?", new DateTime(2020, 12, 31), 0.89m, 0.96m),
                new MarketChoice("Will a federal charge against William Barr be confirmed by Dec. 31, 2020?", new DateTime(2020, 12, 31), 0.96m, 0.99m),
                new MarketChoice("Will NASA find 2020’s global average temperature highest on record?", new DateTime(2021, 1, 31), 0.48m, 0.57m),
                new MarketChoice("Will Trump grant clemency to Paul Manafort in his first term?", new DateTime(2021, 2, 15), 0.55m, 0.55m),
                new MarketChoice("Who will be president of Bolivia on Dec. 31?(Non Luis Arce NO)", new DateTime(2020, 12, 31), 0.96m, 0.98m),
                new MarketChoice("Who will be president of Bolivia on Dec. 31?(Luis Arce YES)", new DateTime(2020, 12, 31), 0.95m, 0.98m),
                new MarketChoice("Will the US give notice of NATO withdrawal in 2020?", new DateTime(2020, 12, 31), 0.97m, 0.98m),
                new MarketChoice("Will Nicolás Maduro be president of Venezuela on Dec. 31, 2020?", new DateTime(2020, 12, 31), 0.93m, 0.98m),
                new MarketChoice("Will Trump meet with Kim Jong-Un in 2020?", new DateTime(2020, 12, 31), 0.95m, 0.98m),
                new MarketChoice("Will Kim Jong-Un be Supreme Leader of North Korea on Dec. 31?", new DateTime(2020, 12, 31), 0.94m, 0.98m),
                new MarketChoice("Will Benjamin Netanyahu be prime minister of Israel on Dec. 31, 2020?", new DateTime(2020, 12, 31), 0.91m, 0.96m),
                new MarketChoice("Will Scottish Parliament call for an independence referendum in 2020?", new DateTime(2021, 1, 1), 0.93m, 0.97m),
                new MarketChoice("Will Nancy Pelosi become Acting U.S. President on January 20?", new DateTime(2021, 1, 20), 0.94m, 0.97m),
                new MarketChoice("Will the Senate confirm a new Fed chair in 2020?", new DateTime(2021, 1, 1), 0.97m, 0.98m),
                new MarketChoice("Will there be more than 9 Supreme Court justices at any point in 2021?", new DateTime(2021, 12, 31), 0.97m, 0.98m),
                new MarketChoice("Which party will win the 2024 U.S. presidential election?(Republican YES)", new DateTime(2021, 12, 31), 0.97m, 0.98m, MarketType.StableFlipShares),
                new MarketChoice("Which party will win the 2024 U.S. presidential election?(Dem NO)", new DateTime(2021, 12, 31), 0.97m, 0.98m, MarketType.StableFlipShares),
                new MarketChoice("Which party will control the Senate after 2022 election?(Republican NO)", new DateTime(2021, 12, 31), 0.97m, 0.98m, MarketType.StableFlipShares),
                new MarketChoice("Which party will control the Senate after 2022 election?(Democratic NO)", new DateTime(2021, 12, 31), 0.97m, 0.98m, MarketType.StableFlipShares),
                new MarketChoice("Who will be the Senate-confirmed Secretary of State on Feb. 15?", new DateTime(2021, 2, 15), 0.97m, 0.98m, MarketType.NegativeRisk),
            };

        private List<MarketChoice> GetMarketsElectionDay() =>  new List<MarketChoice>()
            {
                new MarketChoice("Pennsylvania", new DateTime(2020, 11, 4), 0.67m, 0.77m),   
                new MarketChoice("Florida", new DateTime(2020, 11, 4), 0.67m, 0.77m),   
                new MarketChoice("Arizona", new DateTime(2020, 11, 4), 0.67m, 0.77m),   
                new MarketChoice("Ohio", new DateTime(2020, 11, 4), 0.67m, 0.77m),   
                new MarketChoice("New Hampshire", new DateTime(2020, 11, 4), 0.67m, 0.77m),
                new MarketChoice("Michelle Obama run", new DateTime(2020, 10, 31), 0.97m, 0.99m),   
                new MarketChoice("5% GDP growth", new DateTime(2020, 10, 31), 0.95m, 1.00m),   
            };

        private void PrintOrderedByROI(List<MarketChoice> markets)
        {
            //Annualized--ROI
            Console.WriteLine("Annualized|     |My   |    |");
            Console.WriteLine("       ROI|Price|Value|Days|Market");
            Console.WriteLine("----------|-----|-----|----|------");
            //foreach(var market in markets.OrderByDescending(m => m.Annualized))
            foreach (var market in markets.OrderByDescending(m => m.ExpectedROIAnnualized))
            {
                Console.WriteLine(market.Print());
            }
        }
    }

    public class MarketChoice
    {
        public string Print()
        {
            var roi = $"{ROI:P2}".PadLeft(6);
            //var annualized = $"{AnnualizedReturn():P0}".PadLeft(6);
            var annualized = $"{GetExpectedROIAnnualized():P1}".PadLeft(10);
            //return $"Annualied:{annualized} ROI:{roi} {Name,30}";
            var daysToResolution = (Resolution - DateTime.Today).TotalDays;
            var days = $"{daysToResolution}".PadLeft(3);

            return $"{annualized}|   {Price * 100:0}|   {MyOdds * 100:0}| {days}|{Name,30}";
        }

        public MarketType MarketType { get; set; }
        public string Name { get; set; }
        public DateTime Resolution { get; set; }
        public decimal Price { get; set; }
        public decimal MyOdds { get; set; }
        public decimal ResolutionPayout => Price + (1 - Price) * 0.9m;
        public decimal ExpectedValue => GetExpectedValue();

        public MarketChoice(string name, DateTime resolution, decimal price, decimal myOdds, MarketType type = MarketType.HoldToResolution90_10)
        {
            Name = name;
            Resolution = resolution;
            Price = price;
            MyOdds = myOdds;
            MarketType = type;
        }

        public decimal ROI => (ResolutionPayout / Price) - 1;

        public decimal GetExpectedValue()
        {
            var diff = 1 - Price;
            var resolutionPayout = Price + 0.9m * diff;
            return resolutionPayout * MyOdds;
        }

        public decimal AnnualizedReturn()
        {
            var daysToResolution = (Resolution - DateTime.Today).TotalDays;
            if (daysToResolution <= 0)
                return -1;

            var resolutionsInYear = (decimal)(365 / daysToResolution);
            return resolutionsInYear * ROI;
        }

        public decimal ExpectedROIAnnualized => GetExpectedROIAnnualized();

        public decimal ExpectedROI => (ExpectedValue / Price) - 1;

        public decimal GetExpectedROIAnnualized()
        {
            var daysToResolution = (Resolution - DateTime.Today).TotalDays;
            if (daysToResolution <= 0)
                return -1;

            var resolutionsInYear = (decimal)(365 / daysToResolution);
            return resolutionsInYear * ExpectedROI;
        }

        public decimal Annualized => AnnualizedReturn();

        public string PrintOld()
        {
            var roi = $"{ROI:P2}".PadLeft(6);
            //var annualized = $"{AnnualizedReturn():P0}".PadLeft(6);
            var annualized = $"{GetExpectedROIAnnualized():P0}".PadLeft(6);
            //return $"Annualied:{annualized} ROI:{roi} {Name,30}";
            return $"Annualied ROI:{annualized} ROI:{roi} {Name,30}";
        }
    }

    public enum MarketType
    {
        HoldToResolution90_10,
        NegativeRisk,
        StableFlipShares
    }

}
