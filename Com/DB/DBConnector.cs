using Npgsql;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Shifts_ETL.Com.DB
{
    public static class DBConnector
    {
        static string connectionString = ConfigurationManager.ConnectionStrings["postgres"].ConnectionString;

        public static bool Execute(string sql)
        {
            using (NpgsqlConnection connection = new NpgsqlConnection(connectionString))
            {
                connection.Open();
                NpgsqlTransaction trans = connection.BeginTransaction();
                
                try
                {
                    NpgsqlCommand cmd = new NpgsqlCommand(sql, connection, trans);



                    cmd.Prepare();
                    var x = cmd.ExecuteNonQuery();

                    trans.Save("trans");
                    trans.Commit();
                }
                catch(Exception ex) 
        {

                    trans.Rollback();
                }
                finally
                {

                }
            }

            return true;
        }
    }
}
