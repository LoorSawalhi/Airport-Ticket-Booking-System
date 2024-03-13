using Domain.Models;
using Domain.Repository;
using Infrastructre.Repository;

FlightRepository flightRepository = new FlightRepository("");

IEnumerable<Flight> flights = flightRepository.GetAllFlights();
foreach (var flight in flights)
{
    Console.WriteLine(flight);
}