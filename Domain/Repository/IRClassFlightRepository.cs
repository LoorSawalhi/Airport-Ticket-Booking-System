using Domain.Models;

namespace Domain.Repository;

public interface IRClassFlightRepository
{
    public IEnumerable<FlightDetails> GetFullFlightClassRelation(IEnumerable<Flight> flights, IEnumerable<FlightClass> classes);
    public IEnumerable<ClassFlightRelation> GetAllFlightsClasses();
    public IEnumerable<ClassFlightRelation> FindFlightByFlightId(string flightId);
    public IEnumerable<ClassFlightRelation> FindFlightByClassId(string classId);
    public void Add(ClassFlightRelation flightClass);
    public void Delete(ClassFlightRelation flightClass);
    public ClassFlightRelation Update(ClassFlightRelation newClass, string id);
    public IEnumerable<ClassFlightRelation?> GetFlightByPrice(float minPrice, float maxPrice);

    public IEnumerable<FlightDetails> GetFlightClassesAndPriceByFlight(IEnumerable<FlightInfo> flights,
        IEnumerable<FlightClass> classes, IEnumerable<ClassFlightRelation> flightClassRelation);
}