using RestSharp;
using RestSharp.Authenticators;
using Shifts_ETL.Com.DB;
using Shifts_ETL.Com.Rest;
using Shifts_ETL.Models;
using Shifts_ETL.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Shifts_ETL
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        private static List<Shift> shiftList;

        static void Main(string[] args)
        {
            log.Info("Shifts_ETL Start!");

            int opt;
            do
            {
                opt = -1;
                Console.WriteLine("---------------------------------------------"); 
                Console.WriteLine("1. Get all shifts");
                Console.WriteLine("2. Store all shifts and relational objects");
                Console.WriteLine("3. Get shifts (start-limit)");
                Console.WriteLine("4. ");
                Console.WriteLine("5. ");
                Console.WriteLine("6.");
                Console.WriteLine("7.");
                Console.WriteLine("8.");
                Console.WriteLine("9. Clear all data in db (no turning back :))");
                Console.WriteLine("0. Exit");
                Console.WriteLine("---------------------------------------------");
                Console.WriteLine("Pick an option:");
                opt = int.Parse(Console.ReadLine());

                Console.Clear();

                switch (opt)
                {
                    case 1:
                        GetAllShiftsOption();
                        break;
                    case 2:
                        StoreAllShiftsOption();
                        break;
                    case 3:
                        GetShiftsByPageOption();
                        break;
                    case 4:
                        break;
                    case 5:
                        break;
                    case 6:
                        break;
                    case 7:
                        break;
                    case 8:
                        break;
                    case 9:
                        DeleteAllData();
                        break;
                    case 0:
                        Console.WriteLine("Thanks for useing this app!");
                        Console.ReadLine();
                        return;
                    default:
                        break;
                }

                Console.Clear();
            }
            while (opt != 0);




            //var d = DBConnector.Execute("select * from kpis");
            //var x = RestService.GetShifts();

            //foreach (var item in x)
            //{
            //    DBConnector.StoreShift(item);
            //}



            //using (var db = DBConnector.Create()) 
            //{
            //    var x = db.ExecuteScalar<int>("select count(*) from kpis");
            //}
        }

        private static void GetShiftsByPageOption()
        {
            throw new NotImplementedException();
        }

        private static void StoreAllShiftsOption()
        {
            foreach (var item in shiftList)
            {
                DBConnector.StoreShift(item);
            }
        }

        private static void GetAllShiftsOption()
        {
            Console.WriteLine("Getting all the shifts from endpoint.");
           
            shiftList = RestService.GetShifts();

            Console.WriteLine($"Recieved { shiftList.Count } items.");
        }

        private static void DeleteAllData()
        {
            DBConnector.DeleteAllData();
        }
    }
}
