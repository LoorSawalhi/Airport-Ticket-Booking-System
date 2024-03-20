using Domain.Repository;
using Domain.Service;
using Domain.Service_Interface;
using Infrastructre.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace UserInterface;

public static class ServiceConfiguration
{
    public static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        var basePath = "/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/";
        // Register repositories with their file paths
        services.AddSingleton<IPassengerRepository>(new PassengerRepository($"{basePath}passengers.csv"));
        services.AddSingleton<IAirportRepository>(new AirportRepository("/path/to/airport.csv"));
        services.AddSingleton<IFlightRepository>(new FlightRepository("/path/to/flights.csv"));
        services.AddSingleton<IRClassFlightRepository>(new RClassFlightRepository("/path/to/flight_classes.csv"));

        // Register services
        services.AddSingleton<IPassengerService, PassengerService>();
        services.AddSingleton<IAirportService, AirportService>();
        services.AddSingleton<IFlightService, FlightService>();
        services.AddSingleton<IRClassFlightService, RClassFlightService>();

        return services;
    }
}