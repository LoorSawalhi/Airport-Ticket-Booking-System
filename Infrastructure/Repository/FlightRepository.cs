using System.Globalization;
using CsvHelper;
using CsvHelper.Configuration;
using Domain;
using Domain.CustomException;
using Domain.Models;
using Domain.Repository;

namespace Infrastructre.Repository;

public sealed class FlightRepository(string fileName) : IFlightRepository
{
    public IEnumerable<Flight?> GetAllFlights()
    {
        var config = new CsvConfiguration(CultureInfo.InvariantCulture)
        {
            HasHeaderRecord = true,
            HeaderValidated = null,
        };
        using var reader = new StreamReader(fileName);
        using var csv = new CsvReader(reader, config);
        var records = csv.GetRecords<Flight>();
        return records.ToList();
    }

    public IEnumerable<Flight?> GetAllFlights(SearchState state, IEnumerable<ClassFlightRelation> availableFlights)
    {
        var allFlights = GetAllFlights();
        var availables = from flight in allFlights
            join availableFlight in availableFlights
                on flight.Id equals availableFlight.FlightId
            select flight;
        return availables;
    }

    public Flight FindById(string id)
    {
        return GetAllFlights().FirstOrDefault(flight => flight?.Id == id) ??
               throw new EmptyQueryResultException($"No Flights With Such ID {id}");
    }

    public IEnumerable<Flight> GetFlightsByRelations(IEnumerable<ClassFlightRelation> relations, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights)
    {
        var flights = state == SearchState.All ? GetAllFlights() : GetAllFlights(state, availableFlights);
        return (from flight in flights
            join relation in relations
                on flight.Id equals relation.FlightId
            select flight).Distinct();
    }

    public IEnumerable<Flight> GetFlightByDepartureAirport(IEnumerable<Airport?> departureAirports, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights)
    {
        var flights = state == SearchState.All ? GetAllFlights() : GetAllFlights(state, availableFlights);

        return from flight in flights
            join airport in departureAirports
                on flight.DepartureAirport equals airport.Id
                select flight;
    }

    public IEnumerable<Flight> GetFlightByArrivalAirport(IEnumerable<Airport?> airports, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights)
    {
        var flights = state == SearchState.All ? GetAllFlights() : GetAllFlights(state, availableFlights);
        return from flight in flights
            join airport in airports
                on flight.ArrivalAirport equals airport.Id
            select flight;
    }

    public IEnumerable<ClassFlightRelation> GetFlightByClass(IEnumerable<ClassFlightRelation> relations, SearchState state,
        IEnumerable<ClassFlightRelation> availableFlights)
    {
        var flights = state == SearchState.All ? GetAllFlights() : GetAllFlights(state, availableFlights);
        return from flight in flights
            join relation in relations
                on flight.Id equals relation.FlightId
            select relation;
    }
}