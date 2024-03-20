using Domain.Models;
using Domain.Repository;

namespace UserInterface.Service;

public class FlightClassService
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