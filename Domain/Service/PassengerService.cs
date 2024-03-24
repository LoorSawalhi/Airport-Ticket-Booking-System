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

        // public void CreateFlight(FlightData data)
        // {
        //     // Construct a new Flight entity from data.
        //     var flight = new Flight { /* set properties from data */ };
        //
        //     // Save the new entity using the repository.
        //     _flightRepository.Save(flight);
        // }
}