using PetaPoco;
using System;

namespace Shifts_ETL.Models.DBModels
{
    [PrimaryKey("kpi_name, kpi_date")]

    class Kpis
    {
        public int kpi_id { get; set; }
        public string kpi_name { get; set; }
        public DateTime kpi_date { get; set; }
        public double kpi_value { get; set; }
    }
}
