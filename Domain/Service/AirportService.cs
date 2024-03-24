using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace Domain.Service;

public sealed class AirportService(IAirportRepository airportRepository) : IAirportService
{
    public Airport FindAirportById(string id)
    {
        return airportRepository.FindById(id);
    }

    public IEnumerable<FlightInfo> GetFlightsInfo(IEnumerable<Flight> flights)
    {
        var flightsInfo = airportRepository.GetFlightInfos(flights).ToList();
        CheckListIfEmpty(flightsInfo, $"No Available Infos For Such Flights");
        return flightsInfo;
    }

    public IEnumerable<Airport> FindAirportByCountry(string country)
    {
        var airports = airportRepository.GetAirportByCountry(country).ToList();

        CheckListIfEmpty(airports, $"No Such Airport in {country}");
        return airports;
    }

    public IEnumerable<Airport> GetAllAirports()
    {
        var airports = airportRepository.GetAllAirports().ToList();
        CheckListIfEmpty(airports, "No Available Airports");
        return airports;
    }

    public IEnumerable<Airport> FindAirportByName(string name)
    {
        var airports = airportRepository.GetAirportByName(name).ToList();
        CheckListIfEmpty(airports, "No Available Airports");
        return airports;
    }
}