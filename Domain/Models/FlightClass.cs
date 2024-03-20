namespace Domain.Models;

public class FlightClass
{
    public string Id { get; set; }
    public string Name { get; set; }
    public float MaxPrice { get; set; }
    public float MinPrice { get; set; }
    public int MaxSeats { get; set; }
}