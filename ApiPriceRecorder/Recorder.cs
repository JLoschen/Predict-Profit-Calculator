using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Threading.Tasks;
using ApiPriceRecorder.Services.Abstractions;

namespace ApiPriceRecorder
{
    public class Recorder
    {
        private IPredictItApiService _predictItApiService;
        public Recorder(IPredictItApiService predictItService)
        {
            _predictItApiService = predictItService;
        }

        public async Task Run()
        {

            //Debug.WriteLine("WTF!");
            foreach (var marketId in MarketsToRecord)
            {
                var market = await _predictItApiService.GetMarket(marketId);
                if (market == null)
                {
                    Debug.WriteLine("I'm a failure :(");
                }
                else
                {
                    Debug.WriteLine("Success");
                }
            }
            //_predictItApiService.RunTest();
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
