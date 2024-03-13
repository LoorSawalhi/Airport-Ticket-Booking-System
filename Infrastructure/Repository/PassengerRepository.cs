using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public class PassengerRepository : IPassengerRepository
{
    private string _fileName;

    public PassengerRepository(string fileName)
    {
        _fileName = fileName;
    }

    public IEnumerable<Passenger> GetAllPassengers()
    {
        throw new NotImplementedException();
    }

    public Passenger FindById(string id)
    {
        throw new NotImplementedException();
    }

    public void Add(Passenger passenger)
    {
        throw new NotImplementedException();
    }

    public void Delete(Passenger passenger)
    {
        throw new NotImplementedException();
    }

    public Passenger Update(Passenger newPassenger, string id)
    {
        throw new NotImplementedException();
    }
}