using Newtonsoft.Json;
using System;

namespace ApiPriceRecorder.Model
{
    public class ContractModel
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonProperty("dateEnd")]
        public DateTime? DateEnd { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("status")]
        public ContractStatus Status { get; set; }

        [JsonProperty("lastTradePrice")]
        public float LastTradePrice { get; set; }

        [JsonProperty("bestBuyYesCost")]
        public float BestBuyYesCost { get; set; }

        [JsonProperty("bestBuyNoCost")]
        public float BestBuyNoCost { get; set; }

        [JsonProperty("bestSellYesCost")]
        public float BestSellYesCost { get; set; }

        [JsonProperty("bestSellNoCost")]
        public float BestSellNoCost { get; set; }

        [JsonProperty("lastClosePrice")]
        public float LastClosePrice { get; set; }
    }
}
