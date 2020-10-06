using ApiPriceRecorder.Services;
using ApiPriceRecorder.Services.Abstractions;
using Ninject.Modules;
using System.Net.Http;
using System;
using System.Net.Http.Headers;

namespace ApiPriceRecorder.Ninject
{
    public class PriceRecorderNinjectModule : NinjectModule
    {
        private HttpClient _client;

        public PriceRecorderNinjectModule()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("http://www.predictit.org/api/marketdata/")
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        }

        public override void Load()
        {
            Kernel.Bind<IPredictItApiService>().To<PredictItApiService>();
            Kernel.Bind<HttpClient>().ToConstant(_client);
        }
    }
}