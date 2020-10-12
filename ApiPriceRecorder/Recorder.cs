using System.Diagnostics;
using System.Threading.Tasks;
using ApiPriceRecorder.Model;
using ApiPriceRecorder.Services.Abstractions;

namespace ApiPriceRecorder
{
    public class Recorder
    {
        private IPredictItApiService _predictItApiService;
        private IPredictItDbService _predictItDbService;
        public Recorder(IPredictItApiService predictItService, IPredictItDbService predictItDbService)
        {
            _predictItApiService = predictItService;
            _predictItDbService = predictItDbService;
        }

        public async Task Run()
        {
            //foreach (var marketId in MarketsToRecord)
            //{
            //    var market = await _predictItApiService.GetMarket(marketId);
            //    if (market == null)
            //    {
            //        Debug.WriteLine("I'm a failure :(");
            //    }
            //    else
            //    {
            //        Debug.WriteLine("Success");
            //    }
            //}
            //_predictItApiService.RunTest();

            var newMarket = new MarketDbModel() { Name = "Who will win the super bowl?", Url = "www.goodbet.com" };
            _predictItDbService.InsertMarket(newMarket);

            var markets = _predictItDbService.GetMarkets();
            foreach (var market in markets)
            {
                Debug.WriteLine($"{market.Id} - {market.Name} - {market.Url}");
            }
        }

        private int[] MarketsToRecord { get; } = 
            { 
                //3633, //Dem Nom-closed
                2721,//Which Party will win Presidency
                5542,//Wisconsin
                5597,//Minnesota
                //6874,//2022 Senate
                //2721,//2020 Presidental election
                //3698,//Who will win 2020 presidential market
                //6199,//Which member of Trumps cabinet will leave next
                //5717,//Next European leader out
                //6234,//Will Nasa find 2020's global average temp highest
            }; 
    }
}
