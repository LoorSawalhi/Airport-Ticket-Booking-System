using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace Domain.Service;

public sealed class FlightClassService(IClassRepository classRepository) : IFlightClassService
{
    public IEnumerable<FlightClass> GetAllClasses()
    {
        var classes = classRepository.GetAllClasses();
        CheckListIfEmpty(classes, $"No Available Classes");
        return classes;
    }

    public IEnumerable<FlightClass> GetClassesById(IEnumerable<ClassFlightRelation?> flightRs)
    {
        var classes = classRepository.GetClassesById(flightRs);
        CheckListIfEmpty(classes, $"No Available Classes");
        return classes;
    }

    public FlightClass GetClassByName(string className)
    {
        return classRepository.GetClassByName(className);
    }
}