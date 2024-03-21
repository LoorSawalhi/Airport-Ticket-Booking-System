namespace Domain.Models;

public record FlightInfo(string Id, DateTime DepartureDate, string DepartureAirportName, string ArrivalAirportName)
{
    public string Id { get; set; } = Id;
    public DateTime DepartureDate { get; set; } = DepartureDate;
    public string DepartureAirportName { get; set; } = DepartureAirportName;
    public string ArrivalAirportName { get; set; } = ArrivalAirportName;
}