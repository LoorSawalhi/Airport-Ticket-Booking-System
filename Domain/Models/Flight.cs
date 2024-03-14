namespace Domain.Models;

public class Flight(string id, DateTime departureDate, string departureAirport, string arrivalAirport)
{
    private string _id = id;
    private DateTime _departureDate = departureDate;
    private string _departureAirport = departureAirport;
    private string _arrivalAirport = arrivalAirport;
    public IEnumerable<FlightClass> classes { get; set; }

    public required string id { get; set; }

    public required DateTime departureDate { get; set; }

    public required string departureAirport { get; set; }

    public required string arrivalAirport { get; set; }


    public override string? ToString()
    {
        return $"Flight id = {id}, Departure Date = {departureDate}, Departure Airport = {departureAirport}, Arrival Airport = {arrivalAirport} ";
    }
}