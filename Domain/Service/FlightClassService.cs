using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;

namespace Domain.Service;

public class FlightClassService : IFlightClassService
{
    private readonly IClassRepository _classRepository;

    public FlightClassService(IClassRepository classRepository)
    {
        _classRepository = classRepository;
    }

    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs)
    {
        throw new NotImplementedException();
    }
}