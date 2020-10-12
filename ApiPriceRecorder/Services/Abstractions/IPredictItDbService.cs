using ApiPriceRecorder.Model;
using System.Collections.Generic;

namespace ApiPriceRecorder.Services.Abstractions
{
    public interface IPredictItDbService
    {
        bool InsertMarket(MarketDbModel market);
        IEnumerable<MarketDbModel> GetMarkets();
    }
}
