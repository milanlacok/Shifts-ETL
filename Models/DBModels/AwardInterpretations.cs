using PetaPoco;
using System;

namespace Shifts_ETL.Models.DBModels
{
    [PrimaryKey("award_id", AutoIncrement = false)]
    public class AwardInterpretations
    {
        public Guid award_id { get; set; }
        public Guid shift_id { get; set; }
        public DateTime award_date { get; set; }
        public float award_units { get; set; }
        public double award_cost { get; set; }
    }
}
