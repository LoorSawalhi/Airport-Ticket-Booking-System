namespace Domain.Models;

public class Flight
{
    public string Id { get; set; }
    public DateTime DepartureDate { get; set; }
    public string DepartureAirport { get; set; }
    public string ArrivalAirport { get; set; }


    public override string ToString()
    {
        return
            $"Flight id = {Id}, Departure Date = {DepartureDate.Day}-{DepartureDate.Month}-{DepartureDate.Year}, Departure Airport = {DepartureAirport}, Arrival Airport = {ArrivalAirport}";
    }
}