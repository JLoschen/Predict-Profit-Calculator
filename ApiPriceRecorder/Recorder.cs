using System;
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
            // Console.WriteLine(_predictItApiService.GetTest());
            var result = await _predictItApiService.DoTest();

            if(result != "error")
            {
                Console.WriteLine(result);

                Debug.WriteLine(result);
            }
        }
    }
}
