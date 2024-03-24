using Domain.CustomException;
using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace Domain.Service;

public sealed class RClassFlightService(
    IRClassFlightRepository classFlightRepository,
    IFlightClassService flightClassService)
    : IRClassFlightService
{
    private readonly IFlightClassService _flightClassService = flightClassService;


    public IEnumerable<ClassFlightRelation> FindAllRelations()
    {
        return classFlightRepository.GetAllFlightsClasses();
    }

    public IEnumerable<ClassFlightRelation> FindFlightClassesByFlightId(string flightId)
    {
        var classes = classFlightRepository.FindFlightByFlightId(flightId).ToList();
        CheckListIfEmpty(classes, $"No Available Classes For Such Flight");
        return classes;
    }

    public IEnumerable<FlightDetails> FindFlightClassesAndPrice(IEnumerable<FlightInfo> flights, IEnumerable<FlightClass> classes, IEnumerable<ClassFlightRelation> relations)
    {
        var flightDetails = classFlightRepository.GetFlightClassesAndPriceByFlight(
            flights, classes, relations).ToList();
        CheckListIfEmpty(flightDetails, $"No Available Flights With Such Classes");
        return flightDetails;
    }

    public IEnumerable<ClassFlightRelation> FindFlightsByClassId(string classId)
    {
        var classes = classFlightRepository.FindFlightByClassId(classId).ToList();
        CheckListIfEmpty(classes, $"No Available Flights With Such Class ID {classId}");
        return classes;
    }

    public IEnumerable<ClassFlightRelation?> FindFlightsByPrice(float minPrice, float maxPrice)
    {
        var flights = classFlightRepository.GetFlightByPrice(minPrice, maxPrice).ToList();
        CheckListIfEmpty(flights, $"No Available Flights With Price Range [{minPrice}, {maxPrice}]");
        return flights;
    }
    
    
}