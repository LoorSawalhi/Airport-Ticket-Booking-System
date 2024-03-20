using Domain.Models;

namespace Domain.Service_Interface;

public interface IRClassFlightService
{
    public IEnumerable<ClassFlightRelation?> FindFlightsByClassId(string classId);
    public IEnumerable<ClassFlightRelation?> FindFlightsByPrice(float minPrice, float maxPrice);
    public IEnumerable<ClassFlightRelation?> FindFlightClassesByFlightId(string flightId);
}