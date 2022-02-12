using Newtonsoft.Json;
using System.Collections.Generic;

namespace Shifts_ETL.Models.Response
{
    class ShiftListResponse
    {
        [JsonProperty("results")]
        public List<Shift> Results { get; set; }

        [JsonProperty("start")]
        public int Start { get; set; }

        [JsonProperty("limit")]
        public int Limit { get; set; }

        [JsonProperty("size")]
        public int Size { get; set; }

        [JsonProperty("links")]
        public List<Link> Links { get; set; }
    }
}
