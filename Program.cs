using Shifts_ETL.Com.DB;
using Shifts_ETL.Com.Rest;
using Shifts_ETL.KPIs;
using Shifts_ETL.Models;
using Shifts_ETL.Util;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

namespace Shifts_ETL
{
    class Program
    {
        private static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        private static List<Shift> shiftList;

        static void Main(string[] args)
        {
            log.Debug("Shifts_ETL Start!");

            int opt;
            bool parsed;

            do
            {
                do
                {
                    opt = -1;
                    FunUtils.Logo();
                    Console.WriteLine(" -------------------------------------------------");
                    Console.WriteLine("  1. Get all shifts to memory");
                    Console.WriteLine("  2. Store all shifts to database");
                    Console.WriteLine("  3. Store all shifts to database (optimised)");
                    Console.WriteLine("  4. Calculate defined KPIs");
                    Console.WriteLine("  5. Clear all data in db (no turning back :))");
                    Console.WriteLine("  0. Exit");
                    Console.WriteLine(" -------------------------------------------------");
                    Console.WriteLine("Pick an option:");
                    parsed = int.TryParse(Console.ReadLine(), out opt);

                    Console.Clear();
                } while (!parsed || !opt.In(1, 2, 3, 4, 5, 6, 0));

                switch (opt)
                {
                    case 1:
                        GetAllShiftsOption();
                        break;
                    case 2:
                        StoreAllShiftsOption(false);
                        break;
                    case 3:
                        StoreAllShiftsOption(true);
                        break;
                    case 4:
                        CalculateKPIs();
                        break;
                    case 5:
                        DeleteAllData();
                        break;
                    case 0:
                        Console.WriteLine("Thanks for useing this app!");
                        FunUtils.Credits();
                        AnyKey();
                        return;
                    default:
                        break;
                }

            }
            while (opt != 0);
        }

        private static void CalculateKPIs()
        {
            new NumberOfPaidBreaks().CalculatePaidBreaks();
            new MaxAllowance().CalculateMaxAllowanceInLastTwoWeeks();
            AnyKey();
        }

        private static void StoreAllShiftsOption(bool parallel)
        {
            var sw = new Stopwatch();
            sw.Start();
            if (parallel)
            {
                Parallel.ForEach(shiftList, item =>
                {
                    DBConnector.StoreShift(item);
                });
            }
            else
            {
                foreach (var item in shiftList)
                    DBConnector.StoreShift(item);
            }
            sw.Stop();

            log.Info($"Insert performance (Optimised={parallel}) - {sw.ElapsedMilliseconds}ms");

            AnyKey();
        }



        private static void GetAllShiftsOption()
        {
            log.Info("Getting all the shifts from endpoint.");
           
            shiftList = RestService.GetAllShifts();

            log.Info($"Recieved { shiftList.Count } items.");

            AnyKey();
        }

        private static void DeleteAllData()
        {
            string response;
            do
            {
                Console.Clear();
                Console.WriteLine("Are you really shore you want to do this? (y/n)");
                response = Console.ReadLine();

            } while (!response.ToLower().In("y", "yes", "n", "no"));


            if (response.ToLower().In("y", "yes"))
            {
                DBConnector.DeleteAllShifts();
                DBConnector.DeleteAllKPIs();
                FunUtils.Boom();
                AnyKey();
            }
            else
                Console.Clear();
        }

        private static void AnyKey()
        {
            Console.WriteLine("Press any key...");
            Console.ReadLine();
            Console.Clear();
        }





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
}
