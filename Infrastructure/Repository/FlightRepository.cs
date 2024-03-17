using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;
using Infrastructre.Mapper;

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
        csv.Context.RegisterClassMap<FlightMap>(); 
        var records = csv.GetRecords<Flight>();
        return records.ToList();
    }


    public Flight? FindById(string id)
    {
        return GetAllFlights().FirstOrDefault(flight => flight?.Id == id);
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

    public IEnumerable<Flight?> GetFlightByDepartureAirport(IEnumerable<Airport?> airports)
    {
        var flights = GetAllFlights();

        return from flight in flights
            join airport in airports
                on flight.DepartureAirport equals airport.id
                select flight;
    }

    public IEnumerable<Flight?> GetFlightByArrivalAirport(IEnumerable<Airport?> airports)
    {
        var flights = GetAllFlights();
        return from flight in flights
            join airport in airports
                on flight.ArrivalAirport equals airport.id
            select flight;
    }

    // public IEnumerable<Flight?> GetFlightWithRangePrice(float minPrice, float maxPrice)
    // {
    //     return GetAllFlights().Where(flight => flight != null && flight.);
    // }
}