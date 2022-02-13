using PetaPoco;
using System;

namespace Shifts_ETL.Models.DBModels
{
    [PrimaryKey("break_id", AutoIncrement = false)]

    public class Breaks
    {
        public Guid break_id { get; set; }
        public Guid shift_id { get; set; }
        public DateTime break_start { get; set; }
        public DateTime break_finish { get; set; }
        public bool is_paid { get; set; }
    }
}
