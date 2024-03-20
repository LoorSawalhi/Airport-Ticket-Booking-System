using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;

namespace Domain.Service;

public sealed class AirportService : IAirportService
{
    private readonly IAirportRepository _airportRepository;

    public AirportService(IAirportRepository airportRepository)
    {
        _airportRepository = airportRepository;
    }

    public Airport? FindAirportById(string id)
    {
        return _airportRepository.FindById(id);
    }

    public IEnumerable<Airport?> FindAirportByCountry(string country)
    {
        return _airportRepository.GetAirportByCountry(country);
    }

    public IEnumerable<Airport?> FindAirportByName(string name)
    {
        return _airportRepository.GetAirportByName(name);
    }
}