using Domain.Models;

namespace Domain.Repository;

public interface IRClassFlightRepository
{
    public IEnumerable<ClassFlightRelation> GetAllFlightsClasses();
    public IEnumerable<ClassFlightRelation> FindFlightByFlightId(string flightId);
    public IEnumerable<ClassFlightRelation> FindFlightByClassId(string classId);
    public void Add(ClassFlightRelation flightClass);
    public void Delete(ClassFlightRelation flightClass);
    public ClassFlightRelation Update(ClassFlightRelation newClass, string id);
    public IEnumerable<ClassFlightRelation?> GetFlightByPrice(float minPrice, float maxPrice);

}