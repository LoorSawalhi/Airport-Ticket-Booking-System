using Domain.Models;

namespace Domain.Repository;

public interface IRClassFlightRepository
{
    public IEnumerable<ClassFlightRelation> GetAllFlightsClasses();
    public IEnumerable<ClassFlightRelation> FindFlightByFlightId(string flightId);
    public IEnumerable<ClassFlightRelation> FindFlightByClassId(string classId);
    public IEnumerable<ClassFlightRelation?> GetFlightByPrice(float minPrice, float maxPrice);

    public IEnumerable<FlightDetails> GetFlightClassesAndPriceByFlight(IEnumerable<FlightInfo> flights,
        IEnumerable<FlightClass> classes, IEnumerable<ClassFlightRelation> flightClassRelation);
}