using Domain.Models;

namespace Domain.Service_Interface;

public interface IAirportService
{
    public Airport FindAirportById(string id);
    public IEnumerable<FlightInfo> GetFlightsInfo(IEnumerable<Flight> flights);
    public IEnumerable<Airport> FindAirportByName(string name);
    public IEnumerable<Airport> FindAirportByCountry(string country);
    public IEnumerable<Airport> GetAllAirports();
}