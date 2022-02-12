using Newtonsoft.Json;
using System;

namespace Shifts_ETL.Models
{
    public partial class AwardInterpretation
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("units")]
        public double Units { get; set; }

        [JsonProperty("cost")]
        public double Cost { get; set; }
    }
}
