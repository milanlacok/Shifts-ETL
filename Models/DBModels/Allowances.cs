using PetaPoco;
using System;

namespace Shifts_ETL.Models.DBModels
{
    [PrimaryKey("allowance_id", AutoIncrement = false)]
    public class Allowances
    {
        public Guid allowance_id { get; set; }
        public Guid shift_id { get; set; }
        public float allowance_value { get; set; }
        public double allowance_cost { get; set; }
    }
}
