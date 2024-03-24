using Domain.CustomException;
using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace Domain.Service;

public sealed class FlightClassService(IClassRepository classRepository) : IFlightClassService
{
    public IEnumerable<FlightClass> GetAllClasses()
    {
        var classes = classRepository.GetAllClasses().ToList();
        CheckListIfEmpty(classes, $"No Available Classes");
        return classes;
    }

    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs)
    {
        var classes = classRepository.GetClassesById(flightRs).ToList();
        CheckListIfEmpty(classes, $"No Available Classes");
        return classes;
    }

    public FlightClass GetClassByName(string className)
    {
        return classRepository.GetClassByName(className);
    }

    public int GetClassMaxSeats(string className)
    {
        var maxSeats = classRepository.GetMaxSeats(className);
        if (maxSeats == 0)
            throw new EmptyQueryResultException($"No Available Class With Such Id {className}");

        return maxSeats;
    }
}