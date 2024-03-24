namespace Domain.Models;

public sealed class ClassFlightRelation
{
    public ClassFlightRelation(string flightId, string classId, float price)
    {
        FlightId = flightId;
        ClassId = classId;
        Price = price;
    }

    public ClassFlightRelation()
    {
    }

    public string FlightId { get; set; }
    public string ClassId { get; set; }
    public float Price { get; set; }
}