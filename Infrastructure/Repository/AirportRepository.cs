using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class AirportRepository : IAirportRepository
{
    private string _fileName;

    public AirportRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Airport> GetAllAirports()
    {
        throw new NotImplementedException();
    }

    public Airport FindById(string id)
    {
        throw new NotImplementedException();
    }

    public void Add(Airport airport)
    {
        throw new NotImplementedException();
    }

    public void Delete(Airport airport)
    {
        throw new NotImplementedException();
    }

    public Airport Update(Airport newAirport, string id)
    {
        throw new NotImplementedException();
    }
}