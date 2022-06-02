using NewshoreAir.Domain.JourneyService;
using NewshoreAir.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace PruebaBackEndNewshore.Controllers
{
/// <summary>
/// Controlador de 
/// </summary>
    [RoutePrefix("api/Journey")]
    public class JourneyController : ApiController
    {
        #region Private Fields
        private IJourneyService _journeyService;
        #endregion

        #region Constructor
        public JourneyController(IJourneyService journeyService)
        {
            _journeyService = journeyService;
        }
        #endregion

        /// <summary>
        /// /
        /// </summary>
        /// <param name="Origin">Lugar de origen. Ej: PEI</param>
        /// <param name="Destination">Lugar de destino. Ej: MAD</param>
        /// <returns></returns>
        [HttpGet]
        public Journey GetJourney(string Origin, string Destination)
        {
            return _journeyService.CalculateJourney(Origin, Destination);
        }
    }
}
