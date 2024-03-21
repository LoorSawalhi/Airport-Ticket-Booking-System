namespace Domain.Models;

public sealed record FlightDetails(
    string id,
    DateTime departureDate,
    string departureAirport,
    string arrivalAirport,
    string flightClass,
    float price)
{
    public string id { get; set; } = id;
    public DateTime departureDate { get; set; } = departureDate;
    public string departureAirport { get; set; } = departureAirport;
    public string arrivalAirport { get; set; } = arrivalAirport;
    public string flightClass { get; set; } = flightClass;
    public float price { get; set; } = price;
}