using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.CustomException;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public sealed class ClassRepository(string fileName) : IClassRepository
{
    public IEnumerable<FlightClass> GetAllClasses()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<FlightClass>();
        return records.ToList();
    }

    public IEnumerable<FlightClass> GetClassesExceptId(string classId)
    {
        return GetAllClasses().Where(flightClass => (!flightClass.Id.Equals(classId)));
    }

    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs)
    {
        var classes = GetAllClasses();
        return from flightClass in classes
            join flightR in flightRs
                on flightClass.Id equals flightR.ClassId
            select flightClass;
    }

    public FlightClass GetClassByName(string className)
    {
        return GetAllClasses().FirstOrDefault(classN =>
            classN.Name.Equals(className, StringComparison.InvariantCultureIgnoreCase))
               ?? throw new EmptyQueryResultException($"No Such Class Named {className}");
    }

    public int GetMaxSeats(string className)
    {
        return GetAllClasses().FirstOrDefault(classF =>
            classF.Name.Equals(className, StringComparison.InvariantCultureIgnoreCase))?.MaxSeat ?? 0;
    }
}