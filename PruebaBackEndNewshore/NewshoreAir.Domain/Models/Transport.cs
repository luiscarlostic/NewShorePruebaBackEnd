using Newtonsoft.Json;

namespace NewshoreAir.Domain.Models
{
    public class Transport
    {
        [JsonProperty("flightCarrier")]
        public string FlightCarrier { get; set; }
        [JsonProperty("flightNumber")]
        public string FlightNumber { get; set; }
    }
}
