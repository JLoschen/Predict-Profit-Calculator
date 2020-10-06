using ApiPriceRecorder.Services;
using ApiPriceRecorder.Services.Abstractions;
using Ninject.Modules;
using System.Net.Http;
using System;

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
        }

        public override void Load()
        {
            Kernel.Bind<IPredictItApiService>().To<PredictItApiService>();
            Kernel.Bind<HttpClient>().ToConstant(_client);
        }
    }
}