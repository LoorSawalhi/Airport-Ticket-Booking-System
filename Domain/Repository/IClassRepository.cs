using Domain.Models;

namespace Domain.Repository;

public interface IClassRepository
{
    public IEnumerable<FlightClass> GetAllClasses();
    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs);

    public FlightClass FindById(string id);
    public void Add(FlightClass flightClass);
    public void Delete(FlightClass flightClass);
    public FlightClass Update(FlightClass newClass, string id);
}