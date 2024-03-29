using Domain.Repository;
using Domain.Service;
using Domain.Service_Interface;
using Infrastructre.Repository;
using Microsoft.Extensions.DependencyInjection;

namespace UserInterface;

internal static class ServiceConfiguration
{
    public static IServiceCollection ConfigureServices()
    {
        var services = new ServiceCollection();
        var basePath = "/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/";

        // Register repositories with their file paths
        services.AddSingleton<IPassengerRepository>(new PassengerRepository($"{basePath}passengers.csv"));
        services.AddSingleton<IAirportRepository>(new AirportRepository($"{basePath}airport.csv"));
        services.AddSingleton<IFlightRepository>(new FlightRepository($"{basePath}flights.csv"));
        services.AddSingleton<IRClassFlightRepository>(new RClassFlightRepository($"{basePath}flight_classes.csv"));
        services.AddSingleton<IBookingRepository>(new BookingRepository($"{basePath}bookings.csv"));
        services.AddSingleton<IClassRepository>(new ClassRepository($"{basePath}classes.csv"));

        // Register services
        services.AddSingleton<IPassengerService, PassengerService>();
        services.AddSingleton<IAirportService, AirportService>();
        services.AddSingleton<IFlightService, FlightService>();
        services.AddSingleton<IRClassFlightService, RClassFlightService>();
        services.AddSingleton<IFlightClassService, FlightClassService>();
        services.AddSingleton<IBookingService, BookingService>();

        return services;
    }
}