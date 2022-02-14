using Shifts_ETL.Com.DB;
using Shifts_ETL.Models.DBModels;
using System;

namespace Shifts_ETL.KPIs
{
    public class ShortestShiftDuration
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public void CalculateShortestShift()
        {
            double maxCost = DBConnector.GetShortestShift();

            var kpi = new Kpis
            {
                kpi_name = "min_shift_length_in_hours",
                kpi_value = maxCost,
                kpi_date = DateTime.Now
            };

            if (DBConnector.StoreKPIs(kpi))
                log.Info($"Shortest shift ({maxCost}h) has been stored.");
        }
    }
}
