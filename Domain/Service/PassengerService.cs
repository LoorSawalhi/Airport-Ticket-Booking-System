using Domain.CustomException;
using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;

namespace Domain.Service;

public sealed class PassengerService(IPassengerRepository passengerRepository) : IPassengerService
{
    public Passenger FindPassengerById(string id)
        {
            return passengerRepository.FindById(id) ??
                   throw new EmptyQueryResultException($"No Such Passenger With This ID {id}");
        }
}