using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace Shifts_ETL.Models
{
    public partial class Shift
    {
        [JsonProperty("id")]
        public string Id { get; set; }

        [JsonProperty("date")]
        public string Date { get; set; }

        [JsonProperty("start")]
        public long Start { get; set; }

        [JsonProperty("finish")]
        public long Finish { get; set; }

        [JsonProperty("breaks")]
        public List<Break> Breaks { get; set; }

        [JsonProperty("allowances")]
        public List<Allowance> Allowances { get; set; }

        [JsonProperty("award_interpretations")]
        public List<AwardInterpretation> AwardInterpretations { get; set; }
    }
}
