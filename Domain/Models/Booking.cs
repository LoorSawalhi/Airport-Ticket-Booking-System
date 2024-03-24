namespace Domain.Models;

public record Booking
{
    public Booking(){
    }

    public Booking(string flightId, string classId, string passengerId)
    {
        FlightId = flightId;
        ClassId = classId;
        PassengerId = passengerId;
    }

    public string FlightId { get; set; }
    public string ClassId { get; set; }
    public string PassengerId { get; set; }
}