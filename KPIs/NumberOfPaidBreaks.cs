using Shifts_ETL.Com.DB;
using Shifts_ETL.Models.DBModels;
using System;

namespace Shifts_ETL.KPIs
{
    public class NumberOfPaidBreaks
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void CalculatePaidBreaks()
        {
            int count = DBConnector.GetNumberOfPaidBreaks();

            var kpi = new Kpis
            {
                kpi_name = "total_number_of_paid_breaks",
                kpi_value = count,
                kpi_date = DateTime.Now
            };

            if(DBConnector.StoreKPIs(kpi))
                log.Info($"Total number of paid breaks ({count}) has been stored.");
        }
    }
}
