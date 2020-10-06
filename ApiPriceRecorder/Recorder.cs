using System.Diagnostics;
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

            Debug.WriteLine("WTF!");
            foreach(var marketId in MarketsToRecord)
            {
                var market = await _predictItApiService.GetMarket(marketId);
            }
        }

        private int[] MarketsToRecord { get; } = 
            { 
                3633, //Dem Nom
                2721, //Which Party will win Presidency
                //2721, //Who will win the Presidency
                //2721, //Democratic sweep
            };
    }
}
