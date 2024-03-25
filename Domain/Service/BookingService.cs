using Domain.CustomException;
using Domain.Models;
using Domain.Repository;
using Domain.Service_Interface;
using static Domain.InputHandling;

namespace Domain.Service;

public sealed class BookingService(
    IFlightClassService flightClassService,
    IBookingRepository bookingRepository)
    : IBookingService
{
    public void CreateBooking(FlightDetails flight, string passengerId)
    {
        var maxSeats = flightClassService.GetClassMaxSeats(flight.flightClass);
        var classId = flightClassService.GetClassByName(flight.flightClass);
        var capacity = GetClassCurrentSeats(classId.Id, flight.id);

        if (capacity >= maxSeats)
            throw new EmptyQueryResultException("No Available Seats");

        bookingRepository.AddNewBooking(flight.id, passengerId, classId.Id);
    }

    public void CreateBooking(string flightId, string classId, string passengerId)
    {
        bookingRepository.AddNewBooking(flightId, passengerId, classId);
    }

    public int GetClassCurrentSeats(string classId, string flightId)
    {
        return bookingRepository.GetBookingsCount(flightId, classId);
    }

    public bool IsFlightAvailable(string flightId, string classId)
    {
        var maxSeats = flightClassService.GetClassMaxSeats(classId);
        var capacity = GetClassCurrentSeats(classId, flightId);

        return capacity < maxSeats;
    }

    public IEnumerable<ClassFlightRelation> GetAvailableFlights()
    {
        var classes = flightClassService.GetAllClasses();
        var availableFlights = bookingRepository.GetAvailableFlights(classes).ToList();
        CheckListIfEmpty(availableFlights, $"No Available Flights");
        return availableFlights;
    }

    public IEnumerable<ClassFlightRelation> GetAvailableFlights(Booking booking)
    {
        var classes = flightClassService.GetClassesExceptId(booking.ClassId);
        var availableFlights = bookingRepository.GetAvailableFlights(classes, booking.FlightId).ToList();
        CheckListIfEmpty(availableFlights, $"No Available Flights");
        return availableFlights;
    }

    public IEnumerable<Booking> GetAllBookings(string passengerId)
    {
        var bookings = bookingRepository.GetBookingsById(passengerId).ToList();
        CheckListIfEmpty(bookings, $"Passenger {passengerId} Has No Bookings!");
        return bookings;
    }

    public void CancelBooking(Booking booking)
    {
         bookingRepository.DeleteBooking(booking);
         Console.WriteLine($"{booking} Was Canceled Successfully");
    }

    public void RemoveBooking(Booking booking)
    {
        bookingRepository.DeleteBooking(booking);
    }

    public IEnumerable<BookingDetails> FindBookings(IEnumerable<FlightDetails> flights)
    {
        var filteredFlights = bookingRepository.FilterFlightsByBookings(flights).ToList();
        CheckListIfEmpty(filteredFlights, $"No Available Bookings");
        return filteredFlights;
    }

    public IEnumerable<BookingDetails> FindBookings(IEnumerable<FlightDetails> flights, string passengerId)
    {
        var filteredFlights = bookingRepository.FilterFlightsByPassengerId(flights, passengerId).ToList();
        CheckListIfEmpty(filteredFlights, $"No Available Bookings for Passenger {passengerId}");
        return filteredFlights;
    }
}
