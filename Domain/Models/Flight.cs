namespace Domain.Models;

public class Flight(string id, DateTime departureDate, string departureAirport, string arrivalAirport)
{
    private string _id = id;
    private DateTime _departureDate = departureDate;
    private string _departureAirport = departureAirport;
    private string _arrivalAirport = arrivalAirport;

    public override string? ToString()
    {
        return $"Flight id = {id}, Departure Date = {departureDate}, Departure Airport = {departureAirport}, Arrival Airport = {arrivalAirport} ";
    }
}