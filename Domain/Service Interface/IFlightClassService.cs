using Domain.Models;

namespace Domain.Service_Interface;

public interface IFlightClassService
{
    public IEnumerable<FlightClass> GetAllClasses();
    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs);
    public FlightClass GetClassByName(string className);
    public int GetClassMaxSeats(string className);
}