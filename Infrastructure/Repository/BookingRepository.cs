using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class BookingRepository(string fileName) : IBookingRepository
{
    public IEnumerable<Booking> GetAllBookings()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Booking>();
        return records.ToList();
    }

    public Booking FindById(string id)
    {
        throw new NotImplementedException();
    }

    public void Add(Booking booking)
    {
        throw new NotImplementedException();
    }

    public void Delete(Booking booking)
    {
        throw new NotImplementedException();
    }

    public Booking Update(Booking newBooking, string id)
    {
        throw new NotImplementedException();
    }
}