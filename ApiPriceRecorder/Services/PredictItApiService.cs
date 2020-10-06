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
                using (HttpResponseMessage response = await _predictClient.GetAsync($"/markets/{Id}"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var json = await response.Content.ReadAsStringAsync();

                        return JsonConvert.DeserializeObject<MarketModel>(json);
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

        

        public async Task<string> DoTest()
        {
            try
            {
                //using (HttpResponseMessage response = await _predictClient.GetAsync($"/markets/all"))
                //using (HttpResponseMessage response = await _predictClient.GetAsync($"/markets/{DemNom}"))
                using (HttpResponseMessage response = await _predictClient.GetAsync($"/markets/5"))
                {
                    if (response.IsSuccessStatusCode)
                    {
                        var res = await response.Content.ReadAsStringAsync();

                        var first = res.Substring(0, 4000);
                        return res;
                    }
                    else
                    {
                        Debug.WriteLine("shit");
                    }
                }
            }
            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
                return "error";
            }
            return "confirmed!";
        }
    }
}
