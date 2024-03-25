using Domain.Models;

namespace Domain.Repository;

public interface IClassRepository
{
    public IEnumerable<FlightClass> GetAllClasses();
    public IEnumerable<FlightClass> GetClassesExceptId(string classId);

    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs);

    public FlightClass GetClassByName(string className);
    public int GetMaxSeats(string className);
}