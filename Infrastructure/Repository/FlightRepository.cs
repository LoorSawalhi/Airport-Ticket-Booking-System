using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class FlightRepository : IFlightRepository
{
    private readonly string _fileName;

    public FlightRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Flight?> GetAllFlights()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        using var reader = new StreamReader(_fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Flight>();
        return records.ToList();
    }

    public Flight? FindById(string id)
    {
        return GetAllFlights().FirstOrDefault(flight => flight?.id == id);
    }

    public void Add(Flight flight)
    {
        throw new NotImplementedException();
    }

    public void Delete(Flight flight)
    {
        throw new NotImplementedException();
    }

    public Flight Update(Flight newFlight, string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Flight?> GetFlightByDepartureAirport(string airport)
    {
        return GetAllFlights().Where(flight => flight != null && flight.departureAirport.Equals(airport, StringComparison.InvariantCultureIgnoreCase));
    }
}