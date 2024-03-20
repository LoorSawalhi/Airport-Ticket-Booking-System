using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
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
        throw new NotImplementedException();
    }

    public void Add(Airport airport)
    {
        throw new NotImplementedException();
    }

    public void Delete(Airport airport)
    {
        throw new NotImplementedException();
    }

    public Airport Update(Airport newAirport, string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<Airport> GetAirportByCountry(string country)
    {
        return GetAllAirports().Where(airport => airport.country.Equals(country, StringComparison.InvariantCultureIgnoreCase));
    }
    
    public IEnumerable<Airport> GetAirportByName(string name)
    {
        return GetAllAirports().Where(airport => airport.name.Equals(name, StringComparison.InvariantCultureIgnoreCase));
    }
}