using PetaPoco;
using System;

namespace Shifts_ETL.Models.DBModels
{
    [TableName("shifts")]
    [PrimaryKey("shift_id", AutoIncrement = false)]
    public class Shifts
    {
        public Guid shift_id { get; set; }
        public DateTime shift_date { get; set; }
        public DateTime shift_start { get; set; }
        public DateTime shift_finish { get; set; }
        public double shift_cost { get; set; }
    }
}
