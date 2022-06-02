using NewshoreAir.Domain.Models;

namespace NewshoreAir.Domain.JourneyService
{
    public interface IJourneyService
    {
        Journey CalculateJourney(string origin, string destination);
    }
}
