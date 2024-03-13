using System.Diagnostics;
using Domain.Service;
using Infrastructre.Repository;
using UserInterface.CustomException;
using static UserInterface.Utilities;

namespace UserInterface;

internal class Passenger
{
    public static void Menu()
    {
        var passengerRepository = new PassengerRepository("/home/loor/Desktop/Foothill Training/C#/AirportTicketBookingSystem/Infrastructure/passengers.csv");
        var passengerService = new PassengerService(passengerRepository);
        while (true)
            try
            {
                Console.Write("""
                                  You must be a passenger, Welcome!!
                                  To be able to gain your features, enter your ID.
                                  
                                  Id : 
                                  """);
                var userId = Console.ReadLine();
                Debug.Assert(passengerService != null, nameof(passengerService) + " != null");
                if (userId != null)
                {
                    var passenger = passengerService.FindPassengerById(userId);
                    if (passenger == null)
                        throw new NoAvailablePassenger("No Available Passenger with This ID");

                    PassengerOptions();
                }
                else
                {
                    throw new NotValidOptionsException(InvalidOption);
                }
            }
            catch (NotValidOptionsException e)
            {
                Console.WriteLine(e.Message);
                if (ExitCond() == -1)
                    break;
            }
    }

    private static void PassengerOptions()
    {
        while (true)
            try
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
                var readLine = Console.ReadLine();
                if (readLine != null && int.TryParse(readLine, out var option))
                    Options(option);
                else
                    throw new NotValidOptionsException(InvalidOption);
            }
            catch (NotValidOptionsException e)
            {
                Console.WriteLine(e.Message);
                if (ExitCond() == -1)
                    break;
            }
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
}