using ApiPriceRecorder.Services.Abstractions;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Linq;
using System.IO;
using System.Diagnostics;

namespace ApiPriceRecorder.Services
{
    public class PredictItApiService : IPredictItApiService
    {
        private HttpClient _predictClient;
        public PredictItApiService(HttpClient predictClient)
        {
            _predictClient = predictClient;
        }
        private int DemNom = 3633;
        private int WhichParty = 2721;

        public async Task<string> DoTest()
        {
            try
            {
                //using (HttpResponseMessage response = await _predictClient.GetAsync($"/markets/all"))
                //using (HttpResponseMessage response = await _predictClient.GetAsync($"/markets/{DemNom}"))
                using (HttpResponseMessage response = await _predictClient.GetAsync($"/markets/{WhichParty}"))
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

        public string GetTest()
        {
            
            return "Result";
        }
    }
}
