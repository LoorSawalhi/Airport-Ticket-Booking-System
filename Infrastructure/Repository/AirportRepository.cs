using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.CustomException;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public sealed class AirportRepository(string fileName) : IAirportRepository
{
    public IEnumerable<Airport> GetAllAirports()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Airport>();
        return records.ToList();
    }

    public Airport FindById(string id)
    {
        return GetAllAirports().FirstOrDefault(airport => airport.Id == id) ??
               throw new EmptyQueryResultException($"No Airport With Such ID {id}");
    }

    public IEnumerable<Airport> GetAirportByCountry(string country)
    {
        return GetAllAirports().Where(airport => airport.Country.Equals(country, StringComparison.InvariantCultureIgnoreCase));
    }

    public IEnumerable<Airport> GetAirportByName(string name)
    {
        return GetAllAirports().Where(airport => airport.Name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }

    public IEnumerable<FlightInfo> GetFlightInfos(IEnumerable<Flight> flights)
    {
        var airports = GetAllAirports().ToList();
        return from flight in flights
            join departureAirport in airports
                on flight.DepartureAirport equals departureAirport.Id
            join arrivalAirport in airports
                on flight.ArrivalAirport equals arrivalAirport.Id into fullData
            from data in fullData
            select new FlightInfo(flight.Id, flight.DepartureDate, departureAirport.Name, departureAirport.Name);
    }
}