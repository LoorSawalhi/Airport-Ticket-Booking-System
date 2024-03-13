namespace Domain.Models;

public class Booking(string flightId, string passengerId, string classId)
{
    private string _flightId = flightId;
    private string _passengerId = passengerId;
    private string _classId = classId;
}