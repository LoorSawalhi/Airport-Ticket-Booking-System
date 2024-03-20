namespace Domain.Service_Interface;

public interface IBookingService
{
    public void CreateBooking(string flightId, string passengerId, string flightClass);
}