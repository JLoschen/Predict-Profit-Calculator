using ApiPriceRecorder.Services;
using ApiPriceRecorder.Services.Abstractions;
using Ninject.Modules;
using System.Net.Http;
using System;
using System.Net.Http.Headers;
using ApiPriceRecorder.DataAccess;
using System.Configuration;

namespace ApiPriceRecorder.Ninject
{
    public class PriceRecorderNinjectModule : NinjectModule
    {
        private HttpClient _client;

        public PriceRecorderNinjectModule()
        {
            _client = new HttpClient()
            {
                BaseAddress = new Uri("https://www.predictit.org/api/marketdata/markets/") 
            };

            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json")); /*;charset=utf-8*/
            //_client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("*/*"));
        }

        public override void Load()
        {
            Kernel.Bind<IPredictItApiService>().To<PredictItApiService>();
            Kernel.Bind<HttpClient>().ToConstant(_client);
            Kernel.Bind<IPredictItDbService>().To<PredictItDbService>();
            Bind<IDbConnectionFactory>()
                    .To<DbConnectionFactory>()
                    .WithConstructorArgument("connectionString",
                            ConfigurationManager.ConnectionStrings["PredictItDb"].ConnectionString);
        }
    }
}