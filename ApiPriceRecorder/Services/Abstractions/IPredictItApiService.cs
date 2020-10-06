using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace ApiPriceRecorder.Services.Abstractions
{
    public interface IPredictItApiService
    {
        string GetTest();
        Task<string> DoTest();
    }
}
