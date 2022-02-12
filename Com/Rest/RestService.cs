using RestSharp;
using Shifts_ETL.Models;
using Shifts_ETL.Models.Response;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Diagnostics;
using System.Linq;
using System.Net;

namespace Shifts_ETL.Com.Rest
{
    public static class RestService
    {
        static readonly log4net.ILog log = log4net.LogManager.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);
        static RestClient client = new RestClient(ConfigurationManager.AppSettings["endpointUri"]);

        public static List<Shift> GetShifts()
        {
            log.Debug($"GetShifts Started");

            var request = new RestRequest("/api/shifts", Method.GET);

            return DoExecute<ShiftListResponse>(request).Results;
        }

        public static List<Shift> GetShiftsByPage(int start, int limit)
        {
            log.Debug($"GetShifts Started");

            var endpoint = "/api/shifts";

            var request = new RestRequest(endpoint, Method.GET);
            request.AddParameter("start", start, ParameterType.GetOrPost);
            request.AddParameter("limit", limit, ParameterType.GetOrPost);

            return DoExecute<ShiftListResponse>(request).Results;
        }

        public static List<Shift> GetAllShifts()
        {
            log.Debug($"GetShifts Started");

            var results = new List<Shift>();

            var request = new RestRequest("/api/shifts", Method.GET);
            request.AddParameter("start", 0, ParameterType.GetOrPost);
            request.AddParameter("limit", 30, ParameterType.GetOrPost);

            var response = DoExecute<ShiftListResponse>(request);

            while(response != null && response.Links != null && response.Links.Any() && response.Links[0].Next != string.Empty)
            {
                results.AddRange(response.Results);
                
                request = new RestRequest(response.Links[0].Next, Method.GET);
                response = DoExecute<ShiftListResponse>(request);
            }

            return results;
        }


        private static T DoExecute<T>(RestRequest request) where T : new()
        {
            // Measure duration using Stopwatch
            var stopwatch = Stopwatch.StartNew();

            var response = client.Execute<T>(request);

            stopwatch.Stop();

            log.Debug($"Execution of {request.Resource} took {stopwatch.Elapsed}"); //, response is {response.Content}

            if (response.ErrorException != null)
            {
                const string message = "An error occured when calling the Shifts ETL api service";
                log.Error(message, response.ErrorException);
                var ex = new ApplicationException(message, response.ErrorException);
                throw ex;
            }

            if (response.StatusCode == HttpStatusCode.OK)
            {
                if (response.Data != null)
                {
                    return response.Data;
                }
                else
                {
                    log.Debug($"{request.Resource} response {response.Content}, statusCode {response.StatusCode}");
                    return default(T);
                }
            }
            else
            {
                log.Debug($"{request.Resource} response {response.Content}, statusCode {response.StatusCode}");
                return default(T);
            }
        }
    }
}
