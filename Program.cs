using RestSharp;
using RestSharp.Authenticators;
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

        static void Main(string[] args)
        {
            log.Info("Shifts_ETL Start!");


   
            //var x = RestService.GetAllShifts();

            Console.ReadLine();
        }
    }
}
