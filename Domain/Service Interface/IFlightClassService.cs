using Domain.Models;

namespace Domain.Service_Interface;

public interface IFlightClassService
{
    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs);
}