using Infrastructre.Repository;
using UserInterface.CustomException;
using UserInterface.Service;
using static UserInterface.Utilities;

namespace UserInterface;

internal class Passenger
{
    public const string InvalidPassenger = "No Available Passenger with This ID";
    private static int _inputLine;

    public static void Menu()
    {
        var passengerRepository = new PassengerRepository("/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/passengers.csv");
        var passengerService = new PassengerService(passengerRepository);
        HandleUserOptions<NoAvailablePassenger>(() =>
        {
            Console.Write("""
                          You must be a passenger, Welcome!!
                          To be able to gain your features, enter your ID.

                          Id :
                          """);
            var userId = Console.ReadLine();
            if (userId != null)
            {
                var passenger = passengerService.FindPassengerById(userId);
                if (passenger == null)
                    throw new NoAvailablePassenger(InvalidPassenger);

                PassengerOptions();
            }
            else
            {
                throw new NoAvailablePassenger(InvalidPassenger);
            }
        });
    }

    private static void PassengerOptions()
    {
        HandleUserOptions<NotValidOptionsException>(() =>
        {
            Console.Write("""
                          Welcome Again !! Here are your options

                          1) Book a Flight
                          2) Search for Flights
                          3) Cancel Your Bookings
                          4) Modify Your Booking
                          5) View Personal Bookings
                          6) Log out

                          Option :
                          """);
            _inputLine = ReadOption();
            Options(_inputLine);
        });
    }

    private static void Options(int option)
    {
        switch (option)
        {
            case 1:
                //Book a Flight
                break;
            case 2:
                //Search for Flights
                break;
            case 3:
                //Cancel Your Bookings
                break;
            case 4:
                //Modify Your Booking
                break;
            case 5:
                //View Personal Bookings
                break;
            case 6:
                //Log out
                break;
            default:
                throw new NotValidOptionsException(InvalidOption);
        }
    }

    public void BookAFlight()
    {
        HandleUserOptions<NotValidOptionsException>(() =>
        {
            Console.WriteLine("""
                              To book a flight, search a flight by:
                              1) Departure Country
                              2) Arrival Country
                              3) Price
                              4) Departure Airport
                              5) Arrival Airport
                              6) Class
                              """);
            _inputLine = ReadOption();
        });
    }
}