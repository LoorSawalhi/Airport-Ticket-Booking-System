using Domain.Models;
using Domain.Repository;

namespace UserInterface.Service;

public class AirportServices
{
    private readonly IAirportRepository _airportRepository;

    public AirportServices(IAirportRepository airportRepository)
    {
        _airportRepository = airportRepository;
    }

    public Airport? FindAirportById(string id)
    {
        return _airportRepository.FindById(id);
    }

    internal IEnumerable<Airport?> FindAirportByCountry(string country)
    {
        var airports = _airportRepository.GetAirportByCountry(country);
        //Handle an exception for non existed airports
        return airports;
    }

    internal IEnumerable<Airport?> FindAirportByName(string name)
    {
        var airports = _airportRepository.GetAirportByName(name);
        //Handle an exception for non existed airports
        return airports;
    }
}