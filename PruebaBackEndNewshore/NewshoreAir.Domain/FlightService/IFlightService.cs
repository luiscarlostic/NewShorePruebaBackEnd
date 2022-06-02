using NewshoreAir.Domain.Models;
using System.Collections.Generic;

namespace NewshoreAir.Domain.FlightService
{
    public interface IFlightService
    {
        List<Flight> GetFlights();
    }
}
