using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.CustomException;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public sealed class BookingRepository(string fileName) : IBookingRepository
{
    CsvConfiguration _config = new CsvConfiguration(CultureInfo.InvariantCulture)
    {
        HasHeaderRecord = true,
        HeaderValidated = null,
        MissingFieldFound = null
    };

    public IEnumerable<Booking> GetAllBookings()
    {
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, _config);
        var records = csv.GetRecords<Booking>();
        return records.ToList();
    }

    public void AddNewBooking(string flightId, string passengerId, string flightClass)
    {
        var booking = new Booking(flightId, flightClass, passengerId);

        using var stream = new FileStream(fileName, FileMode.Append, FileAccess.Write, FileShare.None);
        using var writer = new StreamWriter(stream);
        using var csv = new CsvWriter(writer, _config);

        csv.WriteRecord(booking);
        csv.NextRecord();
    }

    public IEnumerable<ClassFlightRelation> GetAvailableFlights(IEnumerable<FlightClass> classes)
    {
        var allBookings = GetAllBookings();
        var flights = from booking in allBookings
            group booking by new { booking.FlightId, booking.ClassId }
            into grouped
            select new
            {
                FlightId = grouped.Key.FlightId,
                ClassId = grouped.Key.ClassId,
                Count = grouped.Count()
            };

        var availableFlights = from flight in flights
            join classf in classes
                on flight.ClassId equals classf.Id
            where flight.Count < classf.MaxSeat
            select new ClassFlightRelation(flight.FlightId, flight.ClassId, 0);

        return availableFlights;
    }

    public IEnumerable<ClassFlightRelation> GetAvailableFlights(IEnumerable<FlightClass> classes, string flightId)
    {
        var allBookings = GetAllBookings();
        var flights = from booking in allBookings
            group booking by new { booking.FlightId, booking.ClassId }
            into grouped
            select new
            {
                FlightId = grouped.Key.FlightId,
                ClassId = grouped.Key.ClassId,
                Count = grouped.Count()
            };

        var availableFlights = from flight in flights
            join classf in classes
                on flight.ClassId equals classf.Id
            where flight.Count < classf.MaxSeat && flight.FlightId == flightId
            select new ClassFlightRelation(flight.FlightId, flight.ClassId, 0);

        return availableFlights;
    }

    public IEnumerable<Booking> GetBookingsById(string passengerId)
    {
        var bookings = GetAllBookings();
        return from booking in bookings
            where booking.PassengerId == passengerId
            select booking;
    }

    public void DeleteBooking(Booking booking)
    {
        var allBookings = GetAllBookings().ToList();
        var bookingToBeDeleted = allBookings.FirstOrDefault(bookings => (booking.ClassId == bookings.ClassId &&
                                                            booking.FlightId == bookings.FlightId &&
                                                            booking.PassengerId == bookings.PassengerId));

        if (bookingToBeDeleted == null)
            throw new EmptyQueryResultException("Booking Cannot Be Found!!");

        allBookings.Remove(bookingToBeDeleted);
        WriteBookings(allBookings);
    }

    public IEnumerable<BookingDetails> FilterFlightsByBookings(IEnumerable<FlightDetails> flights)
    {
        var allBookings = GetAllBookings();
        return from booking in allBookings
            join flight in flights
                on booking.FlightId equals flight.id
            select new BookingDetails(booking.PassengerId, flight.id, flight.departureDate, flight.departureAirport,
                flight.arrivalAirport, flight.flightClass, flight.price);
    }

    public IEnumerable<BookingDetails> FilterFlightsByPassengerId(IEnumerable<FlightDetails> flights, string passengerId)
    {
        var allBookings = GetAllBookings();
        return from booking in allBookings
            join flight in flights
                on new(booking.FlightId, booking.PassengerId) equals (flight.id, passengerId)
            select new BookingDetails(booking.PassengerId, flight.id, flight.departureDate, flight.departureAirport,
                flight.arrivalAirport, flight.flightClass, flight.price);
    }

    private void WriteBookings(IEnumerable<Booking> bookings)
    {
        using var writer = new StreamWriter(fileName);
        using var csv = new CsvWriter(writer, _config);
        csv.WriteRecords(bookings);
    }


    public int GetBookingsCount(string flightId, string classId)
    {
        return GetAllBookings().Count(booking =>
            (booking.ClassId.Equals(classId, StringComparison.InvariantCultureIgnoreCase)
             && booking.FlightId.Equals(flightId, StringComparison.InvariantCultureIgnoreCase)));
    }
}