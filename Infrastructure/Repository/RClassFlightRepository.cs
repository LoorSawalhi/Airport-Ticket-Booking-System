using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public sealed class RClassFlightRepository(string fileName) : IRClassFlightRepository
{
    public IEnumerable<FlightDetails> GetFullFlightClassRelation(IEnumerable<Flight> flights,
        IEnumerable<FlightClass> classes)
    {
        var relations = GetAllFlightsClasses();
        return from flight in flights
            join relation in relations
                on flight.Id equals relation.FlightId
            join flightClass in classes
                on relation.ClassId equals flightClass.Id into details
            from allDetails in details
            select new FlightDetails(flight.Id, flight.DepartureDate, flight.DepartureAirport, flight.ArrivalAirport
            , allDetails.Name, relation.Price);
    }

    public IEnumerable<ClassFlightRelation> GetAllFlightsClasses()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<ClassFlightRelation>();
        return records.ToList();
    }

    public IEnumerable<ClassFlightRelation> FindFlightByFlightId(string flightId)
    {
        return GetAllFlightsClasses().Where(flight => flight.FlightId.Equals(flightId));
    }

    public IEnumerable<ClassFlightRelation> FindFlightByClassId(string classId)
    {
        return GetAllFlightsClasses().Where(flight => flight.ClassId.Equals(classId));
    }

    public IEnumerable<ClassFlightRelation?> GetFlightByPrice(float minPrice, float maxPrice)
    {
        return GetAllFlightsClasses().Where(flight => (flight.Price >= minPrice && flight.Price <= maxPrice));
    }

    public IEnumerable<FlightDetails> GetFlightClassesAndPriceByFlight(IEnumerable<FlightInfo> flights,
        IEnumerable<FlightClass> classes, IEnumerable<ClassFlightRelation> flightClassRelation)
    {
        return from flight in flights
            join flightR in flightClassRelation
                on flight.Id equals flightR.FlightId
            join classInfo in classes
                on flightR.ClassId equals classInfo.Id
            select new FlightDetails(flight.Id, flight.DepartureDate, flight.DepartureAirportName,
                flight.ArrivalAirportName, classInfo.Name, flightR.Price);
    }
}