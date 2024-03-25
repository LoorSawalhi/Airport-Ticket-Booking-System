namespace Domain.Models;

public sealed record BookingDetails(
    string passengerId,
    string flightId,
    DateTime departureDate,
    string departureAirport,
    string arrivalAirport,
    string flightClass,
    float price)
{
    public string passengerId { get; set; } = passengerId;
    public string flightId { get; set; } = flightId;
    public DateTime departureDate { get; set; } = departureDate;
    public string departureAirport { get; set; } = departureAirport;
    public string arrivalAirport { get; set; } = arrivalAirport;
    public string flightClass { get; set; } = flightClass;
    public float price { get; set; } = price;
}