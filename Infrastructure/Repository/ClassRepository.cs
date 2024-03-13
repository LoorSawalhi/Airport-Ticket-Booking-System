using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class ClassRepository : IClassRepository
{
    private string _fileName;

    public ClassRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<FlightClass> GetAllClasses()
    {
        throw new NotImplementedException();
    }

    public FlightClass FindById(string id)
    {
        throw new NotImplementedException();
    }

    public void Add(FlightClass flightClass)
    {
        throw new NotImplementedException();
    }

    public void Delete(FlightClass flightClass)
    {
        throw new NotImplementedException();
    }

    public FlightClass Update(FlightClass newClass, string id)
    {
        throw new NotImplementedException();
    }
}