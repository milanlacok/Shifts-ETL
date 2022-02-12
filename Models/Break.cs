using Newtonsoft.Json;
using System;

namespace Shifts_ETL.Models
{
    public partial class Break
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("finish")]
        public long Finish { get; set; }

        [JsonProperty("paid")]
        public bool Paid { get; set; }
    }
}
