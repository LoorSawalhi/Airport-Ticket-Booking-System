using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class RClassFlightRepository : IRClassFlightRepository{
    private string _fileName;

    public RClassFlightRepository(string fileName)
    {
        _fileName = fileName;
    }


    public IEnumerable<ClassFlightRelation> GetAllFlightsClasses()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(_fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<ClassFlightRelation>();
        return records.ToList();
    }

    public IEnumerable<ClassFlightRelation> FindFlightByFlightId(string flightId)
    {
        return GetAllFlightsClasses().Where(flight => flight.FlightId.Equals(flightId));
    }

    public IEnumerable<ClassFlightRelation> FindFlightByClassId(string classId)
    {
        return GetAllFlightsClasses().Where(flight => flight.FlightId.Equals(classId));
    }

    public void Add(ClassFlightRelation flightClass)
    {
        throw new NotImplementedException();
    }

    public void Delete(ClassFlightRelation flightClass)
    {
        throw new NotImplementedException();
    }

    public ClassFlightRelation Update(ClassFlightRelation newClass, string id)
    {
        throw new NotImplementedException();
    }

    public IEnumerable<ClassFlightRelation?> GetFlightByPrice(float minPrice, float maxPrice)
    {
        return GetAllFlightsClasses().Where(flight => (flight.Price >= minPrice && flight.Price <= maxPrice));
    }
}