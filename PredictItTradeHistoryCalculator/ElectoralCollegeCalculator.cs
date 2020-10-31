using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;

namespace PredictItTradeHistoryCalculator
{
    /// <summary>
    /// Calculates the electoral college for various values in this market:
    /// </summary>
    public class ElectoralCollegeCalculator
    {
        private readonly string _filePath = @"C:\Users\Josh\Documents\PredictIt\538 election-forecasts-2020\presidential_ev_probabilities_2020.csv";
        private const int Biden = 1;
        private const int Trump = 0;

        //280 = 409 - 129 
        //210 = 374 - 164
        //150 = 344 - 194
        //100 = 319 - 219
        //60 = 299 - 239
        //30 = 284 - 254
        //10 = 274 - 264
        //0 = 269 - 269

        public void Run()
        {
            var sims = ReadCsv();

            float total = 0;
            total += PrintTrumpEv(sims, 409, 538);
            total += PrintTrumpEv(sims, 374, 408);
            total += PrintTrumpEv(sims, 344, 373);
            total += PrintTrumpEv(sims, 319, 343);
            total += PrintTrumpEv(sims, 299, 318);
            total += PrintTrumpEv(sims, 284, 298);
            total += PrintTrumpEv(sims, 274, 283);
            total += PrintTrumpEv(sims, 269, 273);

            total += PrintBidenEv(sims, 270, 273);
            total += PrintBidenEv(sims, 274, 283);
            total += PrintBidenEv(sims, 284, 298);
            total += PrintBidenEv(sims, 299, 318);
            total += PrintBidenEv(sims, 319, 343);
            total += PrintBidenEv(sims, 344, 373);
            total += PrintBidenEv(sims, 374, 408);
            total += PrintBidenEv(sims, 409, 538);
            Console.WriteLine($"-------------------------");
            Console.WriteLine($"Total              :{total:P2}");
        }


        private List<EvSimulation> ReadCsv()
        {
            var sims = new List<EvSimulation>();
            using (var file = new StreamReader(_filePath))
            {
                string line;

                while ((line = file.ReadLine()) != null)
                {
                    if (!string.IsNullOrWhiteSpace(line) && !line.Contains("DateExecuted,Type"))
                        sims.Add(new EvSimulation(line));
                }
            }
            return sims;
        }

        private float PrintTrumpEv(List<EvSimulation> sims, short min, short max)
        {
            var candidateSims = sims.Where(e => min <= e.EvCount && e.EvCount <= max);
            var totalOdds = candidateSims.Sum(c => c.TrumpProbability);

            var percent = $"{totalOdds:P1}".PadLeft(5);

            Console.WriteLine($" GOP  By {Diff(min)} - {Diff(max, true)} :{percent}");

            return totalOdds;
        }

        private float PrintBidenEv(List<EvSimulation> sims, short min, short max)
        {
            var candidateSims = sims.Where(e => min <= e.EvCount && e.EvCount <= max);
            var totalOdds = candidateSims.Sum(c => c.BidenProbability);
            var percent = $"{totalOdds:P1}".PadLeft(5);
            Console.WriteLine($" Dems By {Diff(min)} - {Diff(max, true)} :{percent}");
            return totalOdds;
        }

        private string Diff(short ev, bool addOne = false)
        {
            var diff = 538 - ev;
            //Console.WriteLine($"538 - {ev} = {diff}");
            
            var difference = ev - diff;
            if (addOne)
                difference++;
            //Console.WriteLine($"{ev} - {diff} = {difference}");

            return $"{difference}".PadLeft(3);
        }
    }

    public class EvSimulation
    {
        public EvSimulation(string line)
        {
            var props = line.Split(",");

            TrumpProbability = float.Parse(props[0]);
            BidenProbability = float.Parse(props[1]);
            EvCount = short.Parse(props[2]);
        }

        public float TrumpProbability { get; set; }
        public float BidenProbability { get; set; }
        public short EvCount { get;set; }
    }
}
