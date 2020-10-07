using System.Threading.Tasks;

namespace ApiPriceRecorder.Services.Abstractions
{
    public interface IPredictItApiService
    {
        Task<MarketModel> GetMarket(int Id);
        void RunTest();
    }
}
