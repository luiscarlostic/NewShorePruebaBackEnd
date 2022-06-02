using NewshoreAir.Domain.CacheService;
using NewshoreAir.Domain.ConfigurationService;
using NewshoreAir.Domain.LogService;
using NewshoreAir.Domain.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;

namespace NewshoreAir.Domain.FlightService
{
    public class FligthService : IFlightService
    {
        #region Private Fields
        private readonly IConfigurationService _configurationService;
        private readonly ILogService _logService;
        private readonly ICacheService _cacheService;
        private readonly HttpClient _httpClient;
        #endregion

        #region Constructor
        public FligthService(IConfigurationService configurationService, ILogService logService, ICacheService cacheService)
        {
            _configurationService = configurationService;
            _logService = logService;

            if (_httpClient == null)
            {
                _httpClient = new HttpClient() { Timeout = new TimeSpan(0, 0, 120) };
            }
            _cacheService = cacheService;
        }
        #endregion

        public List<Flight> GetFlights()
        {

            var action = "GetFlight";
            var responseString = string.Empty;
            var response = new List<Flight>();
            if (_cacheService.TryGetValue("Flights", out responseString))
            {
                response = JsonConvert.DeserializeObject<List<Flight>>(responseString);
                _logService.Add(LogKey.Begin, action);
                _logService.Add(LogKey.Response, responseString);
                _logService.Add(LogKey.End, action);
                _logService.Generate();
                return response;
            }
            HttpMethod requestType = HttpMethod.Get;

            var queryLevel = _configurationService.GetKey("QueryLevel");
            var url = new Uri(_configurationService.GetKey("FlightsRoutesUrl") + queryLevel);

            using (var message = new HttpRequestMessage(requestType, url))
            {
                Task.Run(async () =>
                {
                    try
                    {
                        var responseMessage = await _httpClient.GetAsync(url);
                        responseString = await responseMessage.Content.ReadAsStringAsync();
                        response = JsonConvert.DeserializeObject<List<Flight>>(responseString);

                        _cacheService.TryAddValue("Flights", JsonConvert.SerializeObject(response));
                    }
                    catch (Exception e)
                    {
                        _logService.Add(LogKey.Begin, action);
                        _logService.Add(LogKey.Url, url.ToString());
                        _logService.Add(LogKey.MethodType, requestType.ToString());
                        _logService.Add(LogKey.Error, $"Trace: {e.StackTrace}, Error Message: {e.Message}");
                        _logService.Add(LogKey.End, action);
                        _logService.Generate();
                        throw new Exception($"Fallo al consumir el servicio: {e.Message}.", e);
                    }
                }).Wait();
            }
            _logService.Add(LogKey.Response, responseString);
            _logService.Add(LogKey.End, action);
            _logService.Generate();

            return response;
        }
    }
}
