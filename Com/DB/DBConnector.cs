using Npgsql;
using PetaPoco;
using Shifts_ETL.Models;
using Shifts_ETL.Models.DBModels;
using Shifts_ETL.Util;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifts_ETL.Com.DB
{
    public static class DBConnector
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

        private const string SHIFT_ID = "shift_id";
        private const string KPI_NAME = "kpi_name";
        private const string KPI_DATE = "kpi_date";

        private static Stopwatch sw;

        private static Database Create()
        {
            sw = new Stopwatch();
            return new Database(connectionString, new PostgreSQLDatabaseProvider());
        }

        public static double GetMaxAllowanceInLastTwoWeeks()
        {
            double maxCost = 0;
            var quary = @"select allowance_cost 
                          from allowances 
                          where allowance_cost = (
                             select Max(a.allowance_cost) 
                             from shifts s
                             join allowances a on s.shift_id = a.shift_id 
                             where s.shift_date BETWEEN
                                 NOW()::DATE-EXTRACT(DOW FROM NOW())::INTEGER-14 
                                 AND NOW()::DATE-EXTRACT(DOW from NOW())::INTEGER)";

            using (var db = Create())
            {
                try
                {
                    sw.Start();
                    db.BeginTransaction();

                    maxCost = db.ExecuteScalar<double>(quary);

                    db.CompleteTransaction();
                    sw.Stop();

                    log.Info($"Execution of GetMaxAllowanceInLastTwoWeeks took {sw.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                    sw.Stop();
                }
            }

            return maxCost;
        }


        public static int GetNumberOfPaidBreaks()
        {
            int count = 0;
            var quary = "select count(*) from breaks where breaks.is_paid = true";

            using (var db = Create())
            {
                try
                {
                    sw.Start();
                    db.BeginTransaction();

                    count = db.ExecuteScalar<int>(quary);

                    db.CompleteTransaction();
                    sw.Stop();

                    log.Info($"Execution of GetNumberOfPaidBreaks took {sw.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                    sw.Stop();
                }
            }

            return count;
        }

        public static int DeleteAllShifts()
        {
            var rows = 0;
            var quary = "delete from shifts";

            using (var db = Create())
            {
                try
                {
                    sw.Start();
                    db.BeginTransaction();

                    rows = db.Execute(quary);
                    
                    db.CompleteTransaction();
                    sw.Stop();

                    log.Info($"Execution of DeleteAllShifts took {sw.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                    sw.Stop();
                }
            }

            return rows;
        }

        public static int DeleteAllKPIs()
        {
            var rows = 0;
            var quary = "delete from kpis";

            using (var db = Create())
            {
                try
                {
                    sw.Start();
                    db.BeginTransaction();

                    rows = db.Execute(quary);

                    db.CompleteTransaction();
                    sw.Stop();

                    log.Info($"Execution of DeleteAllKPIs took {sw.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                    sw.Stop();
                }
            }

            return rows;
        }

        public static void StoreShift(Shift shift)
        {
            using (var db = Create())
            {
                if (db.Fetch<Shifts>($"WHERE { SHIFT_ID }=@0", shift.Id).Any())
                {
                    log.Info($"Shift with Id = \"{shift.Id}\" already exists. Skipping...");
                    return;
                }

                try
                {
                    sw.Start();
                    db.BeginTransaction();

                    var dbShift = CustomMapping.MapShifts(shift);

                    var dbShiftId = db.Insert("shifts", dbShift);

                    foreach(var allowance in shift.Allowances)
                    {
                        var dbAllowance = CustomMapping.MapAllowences(allowance);
                        dbAllowance.shift_id = (Guid)dbShiftId;
                        db.Insert("allowances", dbAllowance);
                    }

                    foreach (var award in shift.AwardInterpretations)
                    {
                        var dbAward = CustomMapping.MapAwards(award);
                        dbAward.shift_id = (Guid)dbShiftId;
                        db.Insert("award_interpretations", dbAward);
                    }

                    foreach (var @break in shift.Breaks)
                    {
                        var dbBreak = CustomMapping.MapBreaks(@break);
                        dbBreak.shift_id = (Guid)dbShiftId;
                        db.Insert("breaks", dbBreak);
                    }

                    db.CompleteTransaction();
                    sw.Stop();

                    log.Info($"Execution of StoreShift took {sw.ElapsedMilliseconds}ms");
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                    sw.Stop();
                }
            }
        }

        public static bool StoreKPIs(Kpis kpi)
        {
            using (var db = Create())
            {
                if (db.Fetch<Kpis>($"WHERE { KPI_NAME }=@0 AND {KPI_DATE}=@1", kpi.kpi_name, kpi.kpi_date.Date ).Any())
                {
                    log.Info($"KPI with kpi_name = \"{ kpi.kpi_name }\" and kpi_date = \"{ kpi.kpi_date.ToShortDateString() }\" already exists. Skipping...");
                    return false;
                }

                try
                {
                    sw.Start();
                    db.BeginTransaction();

                    var dbShiftId = db.Insert("kpis", kpi);

                    db.CompleteTransaction();
                    sw.Stop();

                    log.Info($"Execution of StoreKPIs took {sw.ElapsedMilliseconds}ms");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                    sw.Stop();

                    return false;
                }

                return true;
            }
        }
    }
}
