using Newtonsoft.Json;
using System;

namespace Shifts_ETL.Models
{
    public partial class Allowance
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("value")]
        public double Value { get; set; }

        [JsonProperty("cost")]
        public double Cost { get; set; }
    }
}
