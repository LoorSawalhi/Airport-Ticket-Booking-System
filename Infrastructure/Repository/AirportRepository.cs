using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class AirportRepository : IAirportRepository
{
    private string _fileName;

    public AirportRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Airport> GetAllAirports()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
        };
        using var reader = new StreamReader(_fileName);
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