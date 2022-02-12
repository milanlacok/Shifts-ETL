using Newtonsoft.Json;

namespace Shifts_ETL.Models
{
    public class Link
    {
        [JsonProperty("base")]
        public string Base { get; set; }
        
        [JsonProperty("next")]
        public string Next { get; set; }

        [JsonProperty("prev")]
        public string Prev { get; set; }
    }
}
