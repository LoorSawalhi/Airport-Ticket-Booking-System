using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public sealed class PassengerRepository(string fileName) : IPassengerRepository
{
    public IEnumerable<Passenger?> GetAllPassengers()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = false,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Passenger>();
        return records.ToList();
    }

    public Passenger? FindById(string id)
    {
        return GetAllPassengers().FirstOrDefault(passenger => passenger?.id == id);
    }

    public void Add(Passenger passenger)
    {
        throw new NotImplementedException();
    }

    public void Delete(Passenger passenger)
    {
        throw new NotImplementedException();
    }

    public Passenger Update(Passenger newPassenger, string id)
    {
        throw new NotImplementedException();
    }
}