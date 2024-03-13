using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class BookingRepository : IBookingRepository
{
    private string _fileName;

    public BookingRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Booking> GetAllBookings()
    {
        throw new NotImplementedException();
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