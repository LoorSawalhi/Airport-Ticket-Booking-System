using Domain.Repository;

namespace UserInterface.Service;

internal class PassengerService
{
    private readonly IPassengerRepository _passengerRepository;

        public PassengerService(IPassengerRepository passengerRepository)
        {
            _passengerRepository = passengerRepository;
        }

        public Domain.Models.Passenger? FindPassengerById(string id)
        {
            return _passengerRepository.FindById(id);
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