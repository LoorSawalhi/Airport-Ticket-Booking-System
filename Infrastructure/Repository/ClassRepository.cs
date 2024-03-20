using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class ClassRepository : IClassRepository
{
    private string _fileName;

    public ClassRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<FlightClass> GetAllClasses()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(_fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<FlightClass>();
        return records.ToList();
    }

    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs)
    {
        var classes = GetAllClasses();
        return from classf in classes
            join flightR in flightRs
                on classf.Id equals flightR.ClassId
            select classf;
    }

    public FlightClass FindById(string id)
    {
        throw new NotImplementedException();
    }

    public void Add(FlightClass flightClass)
    {
        throw new NotImplementedException();
    }

    public void Delete(FlightClass flightClass)
    {
        throw new NotImplementedException();
    }

    public FlightClass Update(FlightClass newClass, string id)
    {
        throw new NotImplementedException();
    }
}