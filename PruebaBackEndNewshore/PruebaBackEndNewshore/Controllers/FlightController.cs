using NewshoreAir.Domain.FlightService;
using NewshoreAir.Domain.LogService;
using NewshoreAir.Domain.Models;
using System.Collections.Generic;
using System.Web.Http;

namespace PruebaBackEndNewshore.Controllers
{
    /// <summary>
    /// Controlador de los viajes
    /// </summary>
    [RoutePrefix("api/Flight")]
    public class FlightController : ApiController
    {
        #region Private Fields
        private readonly IFlightService _flightService;
        #endregion

        #region Constructor
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="flightService"></param>
        public FlightController(IFlightService flightService)
        {
            _flightService = flightService;
        }
        #endregion

        #region Methods
        /// <summary>
        /// Obtiene todas las rutas de viaje disponibles.
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public List<Flight> GetRoute()
        {
            var flights = _flightService.GetFlights();
            return flights;
        }
        #endregion
    }
}