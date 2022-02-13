using Npgsql;
using PetaPoco;
using Shifts_ETL.Models;
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

        static Stopwatch sw;

        public static Database Create()
        {
            sw = new Stopwatch();
            return new Database(connectionString, new PostgreSQLDatabaseProvider());
        }

        public static int DeleteAllData()
        {
            var rows = 0;
            var quary = "delete from shifts";

            using (var db = Create())
            {
                try
                {
                    db.BeginTransaction();
                    sw.Start();

                    rows = db.Execute(quary);
                    
                    sw.Stop();
                    db.CompleteTransaction();

                    log.Info($"Execution of DeleteAllData took {sw.Elapsed}");
                }
                catch (Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                }
            }

            return rows;
        }

        public static int StoreShift(Shift shift)
        {
            using (var db = Create())
            {
                try
                {
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
                }
                catch(Exception ex)
                {
                    log.Error(ex.Message, ex);
                    db.AbortTransaction();
                }
            }

            return 1;
        }

        //public static bool Execute(string sql)
        //{
        //    using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
        //    {
        //        connection.Open();
        //        NpgsqlTransaction trans = connection.BeginTransaction();
                
        //        try
        //        {
        //            NpgsqlCommand cmd = new NpgsqlCommand(sql, connection, trans);



        //            cmd.Prepare();
        //            var x = cmd.ExecuteNonQuery();

        //            trans.Save("trans");
        //            trans.Commit();
        //        }
        //        catch(Exception ex) 
        //{

        //            trans.Rollback();
        //        }
        //        finally
        //        {

        //        }
        //    }

        //    return true;
        //}
    }
}
