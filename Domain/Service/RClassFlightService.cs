using Domain.Models;
using Domain.Repository;

namespace UserInterface.Service;

public class RClassFlightService
{
    private readonly IRClassFlightRepository _rClassFlightRepository;

    public RClassFlightService(IRClassFlightRepository classFlightRepository)
    {
        _rClassFlightRepository = classFlightRepository;
    }

    public IEnumerable<ClassFlightRelation?> FindFlightClassesByFlightId(string flightId)
    {
        return _rClassFlightRepository.FindFlightByFlightId(flightId);
    }

    public IEnumerable<ClassFlightRelation?> FindFlightsByClassId(string classId)
    {
        return _rClassFlightRepository.FindFlightByClassId(classId);
    }

    public IEnumerable<ClassFlightRelation?> FindFlightsByPrice(float minPrice, float maxPrice)
    {
        return _rClassFlightRepository.GetFlightByPrice(minPrice, maxPrice);
    }
}