using Domain.Models;

namespace Domain.Repository;

public interface IAirportRepository
{
    public IEnumerable<Airport> GetAllAirports();
    public Airport FindById(string id);
    public void Add(Airport airport);
    public void Delete(Airport airport);
    public Airport Update(Airport newAirport, string id);
    public IEnumerable<Airport> GetAirportByCountry(string country);
    public IEnumerable<Airport> GetAirportByName(string name);
    public IEnumerable<FlightInfo> GetFlightInfos(IEnumerable<Flight> flights);

}
