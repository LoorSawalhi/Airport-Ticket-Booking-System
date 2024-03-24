using Domain.Models;

namespace Domain.Service_Interface;

public interface IBookingService
{
    public void CreateBooking(FlightDetails flight, string passengerId);
    public int GetClassCurrentSeats(string classId, string flightId);
    public bool IsFlightAvailable(string flightId, string classId);
    public IEnumerable<ClassFlightRelation> GetAvailableFlights();
    public IEnumerable<Booking> GetAllBookings(string passengerId);
    public void CancelBooking(Booking booking);
}