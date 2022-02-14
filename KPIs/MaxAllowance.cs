using Shifts_ETL.Com.DB;
using Shifts_ETL.Models.DBModels;
using System;

namespace Shifts_ETL.KPIs
{
    public class MaxAllowance
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void CalculatePaidBreaks()
        {
            double maxCost = DBConnector.GetMaxAllowanceInLastTwoWeeks();

            var kpi = new Kpis
            {
                kpi_name = "max_allowance_cost_14d",
                kpi_value = maxCost,
                kpi_date = DateTime.Now
            };

            if (DBConnector.StoreKPIs(kpi))
                log.Info($"Max allowance cost in the last two weeks ({maxCost}) has been stored.");
        }
    }
}
