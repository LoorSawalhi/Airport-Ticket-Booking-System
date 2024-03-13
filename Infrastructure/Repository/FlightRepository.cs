using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class FlightRepository : IFlightRepository
{
    private string _fileName;

    public FlightRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Flight> GetAllFlights()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false, // Set to false if the file does not have headers
        };
        using (var reader = new StreamReader("/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/flights.csv"))
        using (var csv = new CsvReader(reader, config))
        {
            var records = csv.GetRecords<Flight>();
            return records.ToList();
        }
    }

    public Flight FindById(string id)
    {
        throw new NotImplementedException();
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
}