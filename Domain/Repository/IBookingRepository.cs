using Domain.Models;

namespace Domain.Repository;

public interface IBookingRepository
{
    public IEnumerable<Booking> GetAllBookings();
    public Booking FindById(string id);
    public void Add(Booking booking);
    public void Delete(Booking booking);
    public Booking Update(Booking newBooking, string id);
}