using ApiPriceRecorder.Services.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace ApiPriceRecorder.Services
{
    public class PredictItApiService : IPredictItApiService
    {
        private HttpClient _predictClient;
        public PredictItApiService(HttpClient predictClient)
        {
            _predictClient = predictClient;
        }

        public async Task<MarketModel> GetMarket(int Id)
        {
            try
            {
                using (HttpResponseMessage response = await _predictClient.GetAsync($"{Id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        var cleanedJson = json.Replace("N/A", string.Empty); 
                        return JsonConvert.DeserializeObject<MarketModel>(cleanedJson);
                    }
                    else
                    {

                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine(e.Message);//Log somehow?
            }
            return null;
        }

        public void RunTest()
        {
            try
            {
                var cleanedJson = marketJson.Replace("N/A", string.Empty);
                var market = JsonConvert.DeserializeObject<MarketModel>(cleanedJson);
            }
            catch(Exception e)
            {
                Console.WriteLine("What is you doing?");
            }
        }

        //private string marketJson = "{\"id\": 2721,\"name\": \"Which party will win the 2020 U.S. presidential election?\",\"shortName\": \"Which party wins the presidency in 2020?\",\"image\": \"https://az620379.vo.msecnd.net/images/Markets/66c1cd46-cf5d-48a7-82ca-f6b11031e0ed.png\",\"url\": \"https://www.predictit.org/markets/detail/2721/Which-party-will-win-the-2020-US-presidential-election\",\"contracts\": [{\"id\": 4390,\"dateEnd\": \"N/A\",\"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_dc2c8290-c5d5-4561-8c1a-31762c3972bb.png\",\"name\": \"Democratic\",\r\n      \"shortName\": \"Democratic\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.68,\r\n      \"bestBuyYesCost\": 0.68,\r\n      \"bestBuyNoCost\": 0.33,\r\n      \"bestSellYesCost\": 0.67,\r\n      \"bestSellNoCost\": 0.32,\r\n      \"lastClosePrice\": 0.65,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 4389,\r\n      \"dateEnd\": \"N\/A\",\r\n      \"image\": \"https:\/\/az620379.vo.msecnd.net\/images\/Contracts\/small_2482e38e-80aa-4ff0-b1e2-9c29a0f100bc.png\",\r\n      \"name\": \"Republican\",\r\n      \"shortName\": \"Republican\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.35,\r\n      \"bestBuyYesCost\": 0.36,\r\n      \"bestBuyNoCost\": 0.65,\r\n      \"bestSellYesCost\": 0.35,\r\n      \"bestSellNoCost\": 0.64,\r\n      \"lastClosePrice\": 0.38,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 4388,\r\n      \"dateEnd\": \"N\/A\",\r\n      \"image\": \"https:\/\/az620379.vo.msecnd.net\/images\/Contracts\/small_9d46f92f-fc2d-406a-ade9-8cf2eb41e64e.png\",\r\n      \"name\": \"Libertarian\",\r\n      \"shortName\": \"Libertarian\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.01,\r\n      \"bestBuyYesCost\": 0.01,\r\n      \"bestBuyNoCost\": null,\r\n      \"bestSellYesCost\": null,\r\n      \"bestSellNoCost\": 0.99,\r\n      \"lastClosePrice\": 0.01,\r\n      \"displayOrder\": 0\r\n    },\r\n    {\r\n      \"id\": 4391,\r\n      \"dateEnd\": \"N\/A\",\r\n      \"image\": \"https:\/\/az620379.vo.msecnd.net\/images\/Contracts\/small_5a27f11b-6e7e-4dd6-9d6b-48dafd96b436.png\",\r\n      \"name\": \"Green\",\r\n      \"shortName\": \"Green\",\r\n      \"status\": \"Open\",\r\n      \"lastTradePrice\": 0.01,\r\n      \"bestBuyYesCost\": 0.01,\r\n      \"bestBuyNoCost\": null,\r\n      \"bestSellYesCost\": null,\r\n      \"bestSellNoCost\": 0.99,\r\n      \"lastClosePrice\": 0.01,\r\n      \"displayOrder\": 0\r\n    }\r\n  ],\r\n  \"timeStamp\": \"2020-10-06T23:27:31.5489239\",\r\n  \"status\": \"Open\"\r\n}";
        private string marketJson = "{\"id\": 2721,\"name\": \"Which party will win the 2020 U.S. presidential election?\",\"shortName\": \"Which party wins the presidency in 2020?\",\"image\": \"https://az620379.vo.msecnd.net/images/Markets/66c1cd46-cf5d-48a7-82ca-f6b11031e0ed.png\",\"url\": \"https://www.predictit.org/markets/detail/2721/Which-party-will-win-the-2020-US-presidential-election\",\"contracts\": [{\"id\": 4390,\"dateEnd\": \"N/A\",\"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_dc2c8290-c5d5-4561-8c1a-31762c3972bb.png\",\"name\": \"Democratic\",\"shortName\": \"Democratic\",\"status\": \"Open\",\"lastTradePrice\": 0.68,\"bestBuyYesCost\": 0.68,\"bestBuyNoCost\": 0.33,\"bestSellYesCost\": 0.67,\"bestSellNoCost\": 0.32,\"lastClosePrice\": 0.65,\"displayOrder\": 0},{\"id\": 4389,\"dateEnd\": \"N/A\",\"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_2482e38e-80aa-4ff0-b1e2-9c29a0f100bc.png\",\"name\": \"Republican\",\"shortName\": \"Republican\",\"status\": \"Open\",\"lastTradePrice\": 0.35,\"bestBuyYesCost\": 0.36,\"bestBuyNoCost\": 0.65,\"bestSellYesCost\": 0.35,\"bestSellNoCost\": 0.64,\"lastClosePrice\": 0.38,\"displayOrder\": 0},{\"id\": 4388,\"dateEnd\": \"N/A\",\"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_9d46f92f-fc2d-406a-ade9-8cf2eb41e64e.png\",\"name\": \"Libertarian\",\"shortName\": \"Libertarian\",\"status\": \"Open\",\"lastTradePrice\": 0.01,\"bestBuyYesCost\": 0.01,\"bestBuyNoCost\": null,\"bestSellYesCost\": null,\"bestSellNoCost\": 0.99,\"lastClosePrice\": 0.01,\"displayOrder\": 0},{\"id\": 4391,\"dateEnd\": \"N/A\",\"image\": \"https://az620379.vo.msecnd.net/images/Contracts/small_5a27f11b-6e7e-4dd6-9d6b-48dafd96b436.png\",\"name\": \"Green\",\"shortName\": \"Green\",\"status\": \"Open\",\"lastTradePrice\": 0.01,\"bestBuyYesCost\": 0.01,\"bestBuyNoCost\": null,\"bestSellYesCost\": null,\"bestSellNoCost\": 0.99,\"lastClosePrice\": 0.01,\"displayOrder\": 0}  ],  \"timeStamp\": \"2020-10-06T23:27:31.5489239\",  \"status\": \"Open\"}";
    }
}
