using Newtonsoft.Json;
using System.Text.Json.Serialization;

namespace NewshoreAir.Domain.Models
{
    public class Flight
    {
        //public Transport Transport { get; set; }
        [JsonProperty(PropertyName = "departureStation")]
        public string Origin { get; set; }

        [JsonPropertyName("Destination")]
        public string arrivalStation { get; set; }

        public double Price { get; set; }

        public string FlightCarrier { get; set; }

        public string FlightNumber { get; set; }
    }
}